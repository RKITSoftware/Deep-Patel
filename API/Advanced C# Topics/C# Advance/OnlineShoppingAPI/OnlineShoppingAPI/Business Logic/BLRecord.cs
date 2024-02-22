using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using OfficeOpenXml;
using OnlineShoppingAPI.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace OnlineShoppingAPI.Business_Logic
{
    public class BLRecord
    {
        #region Private Fields

        /// <summary>
        /// _dbFactory is used to store the reference of database connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Static constructor is used to initialize _dbfactory for future reference.
        /// </summary>
        /// <exception cref="ApplicationException">If database can't connect then this exception shows.</exception>
        public BLRecord()
        {
            // Getting data connection from Application state
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            // If database can't be connect.
            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a record of a customer buying products.
        /// </summary>
        /// <param name="objRecord">Order Record containing details of the purchase.</param>
        /// <returns>Create response message.</returns>
        public HttpResponseMessage Create(RCD01 objRecord)
        {
            try
            {
                if (objRecord == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    PRO01 sourceProduct = db.SingleById<PRO01>(objRecord.D01F03);

                    if (sourceProduct == null)
                    {
                        return BLHelper.ResponseMessage(HttpStatusCode.NotFound,
                            "Product not found.");
                    }

                    if (sourceProduct.O01F04 < objRecord.D01F04)
                    {
                        return BLHelper.ResponseMessage(HttpStatusCode.PreconditionFailed,
                            "Product can't be bought because the quantity can't be satisfied.");
                    }

                    // Update source product quantity and calculate total cost
                    sourceProduct.O01F04 -= objRecord.D01F04;
                    objRecord.D01F05 = sourceProduct.O01F03;
                    objRecord.D01F06 = Guid.NewGuid();
                    objRecord.D01F07 = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

                    // Sending Email
                    List<dynamic> lstItems = new List<dynamic>();
                    string customerEmail = db.SingleById<CUS01>(objRecord.D01F02).S01F03;
                    lstItems.Add(new
                    {
                        ProductName = sourceProduct.O01F02,
                        Quantity = objRecord.D01F04,
                        Price = objRecord.D01F05,
                        InvoiceId = objRecord.D01F06,
                        PurchaseTime = objRecord.D01F07
                    });

                    _ = SendMailToUserAsync(customerEmail, lstItems);

                    // Insert order record and update source product
                    db.Insert(objRecord);
                    db.Update(sourceProduct);

                    return BLHelper.ResponseMessage(HttpStatusCode.Created,
                        "Record added successfully.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Creating records of orders from the list of records.
        /// </summary>
        /// <param name="lstNewRecords">List of records</param>
        /// <returns>Create response message</returns>
        //public HttpResponseMessage CreateFromList(List<RCD01> lstNewRecords)
        //{
        //    try
        //    {
        //        if (lstNewRecords.Count == 0)
        //        {
        //            return new HttpResponseMessage(HttpStatusCode.BadRequest)
        //            {
        //                Content = new StringContent("Data is empty")
        //            };
        //        }

        //        using (var db = _dbFactory.OpenDbConnection())
        //        {
        //            foreach (RCD01 item in lstNewRecords)
        //            {
        //                PRO01 sourceProduct = db.SingleById<PRO01>(item.D01F03);

        //                if (sourceProduct != null && sourceProduct.O01F04 >= item.D01F04)
        //                {
        //                    sourceProduct.O01F04 -= item.D01F04;
        //                    item.D01F05 = sourceProduct.O01F03 * item.D01F04;
        //                    item.D01F06 = Guid.NewGuid();

        //                    db.Insert(item);
        //                    db.Update(sourceProduct);
        //                }
        //            }

        //            return new HttpResponseMessage(HttpStatusCode.Created)
        //            {
        //                Content = new StringContent("Records added successfully.")
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception and return an appropriate response
        //        BLHelper.LogError(ex);
        //        return new HttpResponseMessage(HttpStatusCode.InternalServerError)
        //        {
        //            Content = new StringContent("An error occurred while processing the request.")
        //        };
        //    }
        //}

        /// <summary>
        /// Deletes a record from the database.
        /// </summary>
        /// <param name="id">Record id to be deleted.</param>
        /// <returns>Delete response message.</returns>
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BLHelper.ResponseMessage(HttpStatusCode.BadRequest,
                        "Id can't be zero or negative.");
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    RCD01 order = db.SingleById<RCD01>(id);

                    if (order == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound);
                    }

                    // Delete the record by id
                    db.DeleteById<RCD01>(id);

                    return BLHelper.ResponseMessage(HttpStatusCode.OK,
                        "Record deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Gets all record details from the database.
        /// </summary>
        /// <returns>List of records.</returns>
        public List<RCD01> GetAll()
        {
            List<RCD01> lstRecords = new List<RCD01>();

            try
            {
                // Creating a MySqlConnection to connect to the database
                using (MySqlConnection _connection = new MySqlConnection(
                    "Server=localhost;Port=3306;Database=onlineshopping;User Id=Admin;Password=gs@123;"))
                {
                    // Using MySqlCommand to execute SQL command
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM RCD01;", _connection))
                    {
                        _connection.Open();

                        // Using MySqlDataReader to read data from the executed command
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Mapping the data from reader to RCD01 object and adding it to the list
                                lstRecords.Add(new RCD01()
                                {
                                    D01F01 = (int)reader[0],
                                    D01F02 = (int)reader[1],
                                    D01F03 = (int)reader[2],
                                    D01F04 = (int)reader[3],
                                    D01F05 = (int)reader[4],
                                    D01F06 = (Guid)reader[5],
                                    D01F07 = (string)reader[6]
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.LogError(ex);
                return null;
            }

            return lstRecords;
        }

        /// <summary>
        /// Updates record information in the database.
        /// </summary>
        /// <param name="objUpdateRecord">Updated information.</param>
        /// <returns>Update response message.</returns>
        public HttpResponseMessage Update(RCD01 objUpdateRecord)
        {
            try
            {
                if (objUpdateRecord.D01F01 <= 0)
                {
                    return BLHelper.ResponseMessage(HttpStatusCode.BadRequest,
                        "Id can't be zero or negative.");
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    RCD01 existingRecord = db.SingleById<RCD01>(objUpdateRecord.D01F01);

                    if (existingRecord == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound);
                    }

                    // Update record properties
                    existingRecord.D01F05 = existingRecord.D01F05 / existingRecord.D01F04 * objUpdateRecord.D01F04;
                    existingRecord.D01F04 = objUpdateRecord.D01F04;

                    // Perform the database update
                    db.Update(existingRecord);

                    return BLHelper.ResponseMessage(HttpStatusCode.OK,
                        "Record updated successfully.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// For downloading a customer order information using id and giving functionality of 2 types of files.
        /// Json and Excel
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <param name="filetype">File type to download</param>
        /// <returns>Customer order data</returns>
        public HttpResponseMessage Download(int id, string filetype)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    HttpResponseMessage response;

                    // Check the requested file type
                    if (filetype.Equals("Json"))
                    {
                        // Call the JSONResponse method to handle JSON file generation
                        response = JSONResponse(id);
                    }
                    else if (filetype.Equals("Excel"))
                    {
                        // Call the ExcelResponse method to handle Excel file generation
                        response = ExcelResponse(id);
                    }
                    else
                    {
                        // Return a BadRequest response for unsupported file types
                        response = BLHelper.ResponseMessage(HttpStatusCode.BadRequest,
                            "Invalid file type. Supported types are 'Json' and 'Excel'.");
                    }

                    return response;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                BLHelper.LogError(ex);

                // Return a meaningful response for the client in case of an error
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Adds records to the database based on the provided list of items. 
        /// Each item in the list represents a purchase attempt, and if the product is in stock,
        /// a corresponding order record is created, and the product stock is updated accordingly.
        /// </summary>
        /// <param name="lstItems">List of items representing purchase attempts</param>
        /// <returns>Response indicating the success or failure of the purchase operation</returns>
        public HttpResponseMessage AddRecords(List<CRT01> lstItems,
                                                string customerEmail)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    int id = 1;
                    dynamic lstPurchasedItem = new List<dynamic>();

                    foreach (CRT01 item in lstItems)
                    {
                        // Check if the product is in stock
                        PRO01 sourceProduct = db.SingleById<PRO01>(item.T01F03);

                        if (sourceProduct != null && sourceProduct.O01F04 >= item.T01F04)
                        {
                            RCD01 objRecord = new RCD01()
                            {
                                D01F02 = item.T01F02,
                                D01F03 = item.T01F03,
                                D01F04 = item.T01F04,
                                D01F05 = item.T01F05,
                                D01F06 = Guid.NewGuid(),
                                D01F07 = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")
                            };

                            lstPurchasedItem.Add(new
                            {
                                OrderId = id++,
                                ProductName = sourceProduct.O01F02,
                                Quantity = objRecord.D01F04,
                                Price = objRecord.D01F05,
                                InvoiceId = objRecord.D01F06,
                                PurchaseTime = objRecord.D01F07
                            });

                            // Create a new order record
                            db.Insert(objRecord);

                            // Update the product stock
                            sourceProduct.O01F04 -= item.T01F04;

                            db.Update(sourceProduct);

                            // Remove the processed purchase item
                            db.DeleteById<CRT01>(item.T01F01);
                        }
                    }

                    SendMailToUserAsync(customerEmail, lstPurchasedItem);
                }

                // Return a successful response
                return BLHelper.ResponseMessage(HttpStatusCode.OK,
                    "Items bought successfully which are in stock.");
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.LogError(ex);

                // Return a meaningful response for the client in case of an error
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the request.");
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Asynchronously sends an email to a customer with an attached Excel file containing order details.
        /// </summary>
        /// <param name="customerEmail">The email address of the customer.</param>
        /// <param name="lstPurchasedItem">The list of purchased items to include in the Excel file.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        private async Task SendMailToUserAsync(string customerEmail, dynamic lstPurchasedItem)
        {
            // Generate an HTTP response with an attached Excel file
            HttpResponseMessage response = HttpExcelResponse(lstPurchasedItem);

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
                smtpClient.Credentials = HttpContext.Current.Application["Credentials"]
                    as NetworkCredential;
                smtpClient.EnableSsl = true;

                try
                {
                    smtpClient.Send(mail);
                    Console.WriteLine("Email sent successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error sending email: " + ex.Message);
                }
                finally
                {
                    // Dispose of resources
                    mail.Dispose();
                    excelStream.Dispose();
                }
            }
        }

        /// <summary>
        /// Generates an HTTP response containing an Excel file with order details based on the provided list of purchased items.
        /// </summary>
        /// <param name="lstPurchasedItem">The list of purchased items to include in the Excel file.</param>
        /// <returns>An HttpResponseMessage containing the Excel file with order details.</returns>
        private HttpResponseMessage HttpExcelResponse(dynamic lstPurchasedItem)
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

                foreach (dynamic item in lstPurchasedItem)
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
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Order Receipt.xlsx"
                };

                // Set content type for Excel file
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                return response;
            }
        }

        /// <summary>
        /// Creates a JSON response for a view model containing order details of a specific customer.
        /// </summary>
        /// <param name="id">Customer ID.</param>
        /// <returns>JSON attachment response.</returns>
        private HttpResponseMessage JSONResponse(int id)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Define SQL expression for joining tables (RCD01, PRO01, CUS01)
                    SqlExpression<RCD01> joinSql = db.From<RCD01>()
                        .Join<PRO01>((r, p) => r.D01F03 == p.O01F01)
                        .Join<CUS01>((r, c) => r.D01F02 == c.S01F01);

                    // Execute the SQL query to retrieve order details for the specified customer ID
                    var result = db.SelectMulti<RCD01, PRO01, CUS01>(joinSql)
                        .Where((r) => r.Item1.D01F02 == id)
                        .Select((r) => new
                        {
                            OrderId = r.Item1.D01F01,
                            CustomerName = r.Item3.S01F02,
                            ProductName = r.Item2.O01F02,
                            Price = r.Item1.D01F05,
                            Quantity = r.Item1.D01F04,
                            InvoiceId = r.Item1.D01F06
                        })
                        .ToList();

                    // Check if any data is found for the specified criteria
                    if (result.Count == 0)
                    {
                        // Return a NotFound response if no data is found
                        return BLHelper.ResponseMessage(HttpStatusCode.NotFound,
                            "No data found for the specified criteria.");
                    }

                    // Serialize the result to JSON format
                    string jsonData = JsonConvert.SerializeObject(result, Formatting.Indented);

                    // Create an HTTP response with JSON content
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StringContent(jsonData);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // Set content disposition for attachment with a filename based on the customer name
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = result[0].CustomerName + " Order Data.json"
                    };

                    return response;
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an InternalServerError response
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Creates an Excel response for a view model containing order details of a specific customer.
        /// </summary>
        /// <param name="id">Customer ID.</param>
        /// <returns>Excel attachment response.</returns>
        private HttpResponseMessage ExcelResponse(int id)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Join tables to get order details along with customer and product information
                    SqlExpression<RCD01> joinSql = db.From<RCD01>()
                        .Join<PRO01>((r, p) => r.D01F03 == p.O01F01)
                        .Join<CUS01>((r, c) => r.D01F02 == c.S01F01);

                    // Retrieve data from the database based on the specified customer ID
                    var result = db.SelectMulti<RCD01, PRO01, CUS01>(joinSql)
                        .Where((r) => r.Item1.D01F02 == id)
                        .Select((r) => new
                        {
                            CustomerName = r.Item3.S01F02,
                            ProductName = r.Item2.O01F02,
                            Price = r.Item1.D01F05,
                            Quantity = r.Item1.D01F04,
                            InvoiceId = r.Item1.D01F06,
                            PurchaseTime = r.Item1.D01F07
                        })
                        .ToList();

                    // Check if any data is found for the specified criteria
                    if (result.Count == 0)
                    {
                        return BLHelper.ResponseMessage(HttpStatusCode.NotFound,
                            "No data found for the specified criteria.");
                    }

                    return HttpExcelResponse(result);
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the request.");
            }
        }

        #endregion
    }
}