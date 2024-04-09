using OfficeOpenXml;
using OnlineShoppingAPI.BL.Common.Interface;
using OnlineShoppingAPI.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace OnlineShoppingAPI.BL.Common.Service
{
    public class BLEmail : IEmailService
    {
        public async Task SendAsync(string customerEmail, List<dynamic> lstItems)
        {
            HttpResponseMessage response = HttpExcelResponse(lstItems);

            // Read the content from HttpResponseMessage
            byte[] excelData = await response.Content.ReadAsByteArrayAsync();

            // Create a MemoryStream from the Excel data
            using (MemoryStream excelStream = new MemoryStream(excelData))
            {
                // Create a MailMessage
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("deeppatel2513@outlook.com");
                mail.To.Add(customerEmail);
                mail.Subject = "Order Receipt";
                //mail.Body = "Body of the Email";
                mail.IsBodyHtml = true;

                // Attach the Excel file to the MailMessage
                Attachment attachment = new Attachment(excelStream,
                    $"Receipt {DateTime.Now:dd-MM-yyyy}.xlsx",
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                mail.Attachments.Add(attachment);

                // Send the email using SMTP
                SmtpClient smtpClient = new SmtpClient("smtp.office365.com");
                smtpClient.Port = 587;
                smtpClient.Credentials =
                    HttpContext.Current.Application["Credentials"] as NetworkCredential;
                smtpClient.EnableSsl = true;

                try
                {
                    smtpClient.Send(mail);
                    Console.WriteLine("Email sent successfully.");
                }
                catch (Exception ex)
                {
                    ex.LogException();
                }
                finally
                {
                    // Dispose of resources
                    mail.Dispose();
                    excelStream.Dispose();
                }
            }
        }

        public void Send(string email, string otp)
        {
            try
            {
                // Initialize SMTP client with Office 365 settings.
                SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587);
                smtpClient.Credentials = HttpContext.Current
                    .Application["Credentials"] as NetworkCredential;

                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;

                // Create a mail message with sender, recipient, subject, and body.
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("deeppatel2513@outlook.com", "Deep Patel");
                mailMessage.To.Add(new MailAddress(email));

                mailMessage.Subject = "OTP for Buying";
                mailMessage.Body = $"OTP for buying items in your cart: {otp}";

                // Send the mail message.
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                ex.LogException();
            }
        }

        private HttpResponseMessage HttpExcelResponse(dynamic lstItems)
        {
            // Set the license context for EPPlus (ExcelPackage)
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Create an Excel package
            using (var package = new ExcelPackage())
            {
                // Add a worksheet named "DataSheet"
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("DataSheet");

                // Set column headers
                worksheet.Cells["A1"].Value = "Order No.";
                worksheet.Cells["B1"].Value = "Product Name";
                worksheet.Cells["C1"].Value = "Price";
                worksheet.Cells["D1"].Value = "Quantity";
                worksheet.Cells["E1"].Value = "Invoice Id";
                worksheet.Cells["F1"].Value = "Purchase Time";

                // Populate the worksheet with data
                int row = 2;
                int number = 1;
                int totalPrice = 0;
                int totalQuantity = 0;

                foreach (dynamic item in lstItems)
                {
                    worksheet.Cells[row, 1].Value = number++;
                    worksheet.Cells[row, 2].Value = item.ProductName;
                    worksheet.Cells[row, 3].Value = item.Price;
                    worksheet.Cells[row, 4].Value = item.Quantity;
                    worksheet.Cells[row, 5].Value = item.InvoiceId;
                    worksheet.Cells[row, 6].Value = item.PurchaseTime;

                    row++;
                    totalPrice += item.Price * item.Quantity;
                    totalQuantity += item.Quantity;
                }

                // Add total row
                row++;
                worksheet.Cells[row, 2].Value = "Total";
                worksheet.Cells[row, 3].Value = totalPrice;
                worksheet.Cells[row, 4].Value = totalQuantity;

                // Get the Excel content as a byte array
                byte[] content = package.GetAsByteArray();

                // Create an HTTP response with the Excel content
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(content)
                };

                // Set content disposition for attachment
                response.Content.Headers.ContentDisposition =
                    new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "Order Receipt.xlsx"
                    };

                // Set content type for Excel file
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                return response;
            }
        }
    }
}