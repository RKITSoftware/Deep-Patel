using Newtonsoft.Json;
using OfficeOpenXml;
using OnlineShoppingAPI.BL.Common;
using OnlineShoppingAPI.BL.Common.Interface;
using OnlineShoppingAPI.BL.Common.Service;
using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.DL;
using OnlineShoppingAPI.Extension;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;
using OnlineShoppingAPI.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using static OnlineShoppingAPI.BL.Common.BLHelper;

namespace OnlineShoppingAPI.BL.Service
{
    /// <summary>
    /// Service implementation of <see cref="IRCD01Service"/> interface.
    /// </summary>
    public class BLRCD01 : IRCD01Service
    {
        #region Private Fields

        /// <summary>
        /// Instance of <see cref="RCD01"/> for create or delete operation.
        /// </summary>
        private RCD01 _objRCD01;

        /// <summary>
        /// Orm Lite Connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Services of <see cref="IPFT01Service"/>
        /// </summary>
        private readonly IPFT01Service _pft01Service;

        /// <summary>
        /// Services of Email for email sent.
        /// </summary>
        private readonly IEmailService _emailService;

        /// <summary>
        /// DB Context for MySQL related query execution.
        /// </summary>
        private readonly DBRCD01 _dbRCD01;

        #endregion

        #region Public Properties

        /// <summary>
        /// Specifies the operation to perform.
        /// </summary>
        public EnmOperation Operation { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize fields of <see cref="BLRCD01"/>.
        /// </summary>
        public BLRCD01()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            _pft01Service = new BLPFT01();
            _emailService = new BLEmail();
            _dbRCD01 = new DBRCD01();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialize objects which are needed for create or update operation.
        /// </summary>
        /// <param name="objDTORCD01">DTO of RCD01</param>
        public void PreSave(DTORCD01 objDTORCD01)
        {
            _objRCD01 = objDTORCD01.Convert<RCD01>();

            if (Operation == EnmOperation.Create)
            {
                _objRCD01.D01F06 = Guid.NewGuid();
                _objRCD01.D01F07 = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            }
        }

        /// <summary>
        /// Performs the create or update operation.
        /// </summary>
        /// <param name="response"></param>
        public void Save(out Response response)
        {
            if (Operation == EnmOperation.Create)
                Create(out response);
            else
                response = OkResponse("Record updated successfully.");
        }

        /// <summary>
        /// Validates the objects before the save process.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        /// <returns>True if validation successful, else false.</returns>
        public bool Validation(out Response response)
        {
            response = null;
            return true;
        }

        /// <summary>
        /// Deletes the record from the database.
        /// </summary>
        /// <param name="id">Record Id.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void Delete(int id, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    RCD01 order = db.SingleById<RCD01>(id);

                    if (order == null)
                    {
                        response = NotFoundResponse("Record not found.");
                        return;
                    }

                    db.DeleteById<RCD01>(id);
                    response = OkResponse("Record deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
        }

        /// <summary>
        /// Generate a <see cref="HttpResponseMessage"/> containing the download response as attachment.
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <param name="filetype">File that user wants to download.</param>
        /// <returns></returns>
        public HttpResponseMessage Download(int id, string filetype)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (filetype.Equals("Json"))
                    {
                        return JSONResponse(id);
                    }
                    else if (filetype.Equals("Excel"))
                    {
                        return ExcelResponse(id);
                    }

                    return ResponseMessage(HttpStatusCode.BadRequest,
                        "Invalid file type. Supported types are 'Json' and 'Excel'.");
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                return ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Retrieves all records information.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void GetAllRecord(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    List<RCD01> lstRCD01 = db.Select<RCD01>();

                    if (lstRCD01 == null || lstRCD01.Count == 0)
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.NoContent,
                            Message = "No Records."
                        };
                    }
                    else
                    {
                        response = OkResponse("Success.");
                        response.Data = lstRCD01;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
        }

        /// <summary>
        /// Buy all items of customer from the cart.
        /// </summary>
        /// <param name="lstItems">List of items that customer wants to buy.</param>
        /// <param name="s01F03">customer Email Address.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        /// <returns>True for successful completion, else false if error occurs.</returns>
        public bool BuyCartItems(List<CRT01> lstItems, string s01F03, out Response response)
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
                        PRO02 sourceProduct = db.SingleById<PRO02>(item.T01F03);

                        if (sourceProduct != null && sourceProduct.O02F05 >= item.T01F04)
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
                                ProductName = sourceProduct.O02F02,
                                Quantity = objRecord.D01F04,
                                Price = objRecord.D01F05,
                                InvoiceId = objRecord.D01F06,
                                PurchaseTime = objRecord.D01F07
                            });

                            // Create a new order record
                            db.Insert(objRecord);

                            // Update the product stock
                            sourceProduct.O02F05 -= item.T01F04;
                            if (sourceProduct.O02F05 == 0)
                            {
                                sourceProduct.O02F07 = (int)EnmProductStatus.OutOfStock;
                            }

                            db.Update(sourceProduct);
                            _pft01Service.UpdateProfit(sourceProduct, objRecord.D01F04);

                            db.DeleteById<CRT01>(item.T01F01);
                        }
                    }

                    _emailService.SendAsync(s01F03, lstPurchasedItem);
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
                return false;
            }

            response = OkResponse("Items bought successfully.");
            return true;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates an <see cref="HttpResponseMessage"/> with excel file as attachment.
        /// </summary>
        /// <param name="lstPurchasedItem">List of purchased item of customer.</param>
        /// <returns><see cref="HttpResponseMessage"/> containing order receipt.</returns>
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

        /// <summary>
        /// Creates a record.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        private void Create(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    PRO02 sourceProduct = db.SingleById<PRO02>(_objRCD01.D01F03);

                    // Update source product quantity and calculate total cost
                    sourceProduct.O02F05 -= _objRCD01.D01F04;
                    if (sourceProduct.O02F05 == 0)
                    {
                        sourceProduct.O02F07 = (int)EnmProductStatus.OutOfStock;
                    }

                    _objRCD01.D01F05 = sourceProduct.O02F04;

                    List<dynamic> lstItems = new List<dynamic>();
                    string customerEmail = db.SingleById<CUS01>(_objRCD01.D01F02).S01F03;
                    lstItems.Add(new
                    {
                        ProductName = sourceProduct.O02F02,
                        Quantity = _objRCD01.D01F04,
                        Price = _objRCD01.D01F05,
                        InvoiceId = _objRCD01.D01F06,
                        PurchaseTime = _objRCD01.D01F07
                    });

                    _emailService.SendAsync(customerEmail, lstItems);

                    // Insert order record and update source product
                    db.Insert(_objRCD01);
                    db.Update(sourceProduct);

                    _pft01Service.UpdateProfit(sourceProduct, _objRCD01.D01F04);

                    response = OkResponse("Record successfully created.");
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
        }

        /// <summary>
        /// Gets the customer records using OrmLite.
        /// </summary>
        /// <param name="customerId">Customer id</param>
        /// <returns>A dynamic result containing the customer records.</returns>
        private dynamic JoinOfRcdProCus(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                // Define SQL expression for joining tables (RCD01, PRO02, CUS01)
                SqlExpression<RCD01> joinSql = db.From<RCD01>()
                    .Join<PRO02>((r, p) => r.D01F03 == p.O02F01)
                    .Join<CUS01>((r, c) => r.D01F02 == c.S01F01);

                // Execute the SQL query to retrieve order details for the specified customer ID
                var result = db.SelectMulti<RCD01, PRO02, CUS01>(joinSql)
                        .Where((r) => r.Item1.D01F02 == id)
                        .Select((r) => new
                        {
                            OrderId = r.Item1.D01F01,
                            CustomerName = r.Item3.S01F02,
                            ProductName = r.Item2.O02F02,
                            Price = r.Item1.D01F05,
                            Quantity = r.Item1.D01F04,
                            InvoiceId = r.Item1.D01F06,
                            PurchaseTime = r.Item1.D01F07
                        })
                        .ToList();

                return result;
            }
        }

        /// <summary>
        /// Generates a excel file response according to the specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private HttpResponseMessage ExcelResponse(int id)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    dynamic result = JoinOfRcdProCus(id);

                    // Check if any data is found for the specified criteria
                    if (result.Count == 0)
                    {
                        return ResponseMessage(HttpStatusCode.NotFound,
                            "No data found for the specified criteria.");
                    }

                    return HttpExcelResponse(result);
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                return ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Creates a JSON <see cref="HttpResponseMessage"/> containing the customer record data.
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns><see cref="HttpResponseMessage"/> with customer records data.</returns>
        private HttpResponseMessage JSONResponse(int id)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    DataTable dtCUS01RCD01Data = _dbRCD01.GetCUS01Data(id);

                    // Check if any data is found for the specified criteria
                    if (dtCUS01RCD01Data.Rows.Count == 0)
                    {
                        return BLHelper.ResponseMessage(HttpStatusCode.NotFound,
                            "No data found for the specified criteria.");
                    }

                    // Serialize the result to JSON format
                    string jsonData = JsonConvert.SerializeObject(dtCUS01RCD01Data, Formatting.Indented);

                    // Create an HTTP response with JSON content
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(jsonData)
                    };
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // Set content disposition for attachment with a filename based on the customer name
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "Records.json"
                    };

                    return response;
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                return ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the request.");
            }
        }

        #endregion
    }
}