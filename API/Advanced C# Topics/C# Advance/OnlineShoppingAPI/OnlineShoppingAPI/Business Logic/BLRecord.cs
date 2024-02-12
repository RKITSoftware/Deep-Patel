using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using OfficeOpenXml;
using OnlineShoppingAPI.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace OnlineShoppingAPI.Business_Logic
{
    public class BLRecord
    {
        /// <summary>
        /// _dbFactory is used to store the reference of database connection.
        /// </summary>
        private static readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Static constructor is used to initialize _dbfactory for future reference.
        /// </summary>
        /// <exception cref="ApplicationException">If database can't connect then this exception shows.</exception>
        static BLRecord()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        /// <summary>
        /// Creating a record of which customer buys which products.
        /// </summary>
        /// <param name="objRecord">Order Record</param>
        /// <returns>Create response message</returns>
        internal HttpResponseMessage Create(RCD01 objRecord)
        {
            if (objRecord == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            using (var db = _dbFactory.OpenDbConnection())
            {
                PRO01 sourceProduct = db.SingleById<PRO01>(objRecord.D01F03);

                if (sourceProduct != null)
                {
                    if (sourceProduct.O01F04 >= objRecord.D01F04)
                    {
                        sourceProduct.O01F04 -= objRecord.D01F04;
                        objRecord.D01F05 = sourceProduct.O01F03 * objRecord.D01F04;
                        objRecord.D01F06 = Guid.NewGuid();
                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.PreconditionFailed)
                        {
                            Content = new StringContent("Product can't be buy because quantity can't be satisfied.")
                        };
                    }
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Product not found.")
                    };
                }

                db.Insert(objRecord);
                db.Update(sourceProduct);

                return new HttpResponseMessage(HttpStatusCode.Created)
                {
                    Content = new StringContent("Record added successfully")
                };
            }
        }

        /// <summary>
        /// Creating records of orders from the list of records.
        /// </summary>
        /// <param name="lstNewRecords">List of records</param>
        /// <returns>Create response message</returns>
        internal HttpResponseMessage CreateFromList(List<RCD01> lstNewRecords)
        {
            if (lstNewRecords.Count == 0)
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Data is empty")
                };

            using (var db = _dbFactory.OpenDbConnection())
            {
                foreach (RCD01 item in lstNewRecords)
                {
                    PRO01 sourceProduct = db.SingleById<PRO01>(item.D01F03);

                    if(sourceProduct == null)
                        continue;

                    if (sourceProduct.O01F04 < item.D01F04)
                        continue;

                    sourceProduct.O01F04 -= item.D01F04;
                    item.D01F05 = sourceProduct.O01F03 * item.D01F04;
                    item.D01F06 = Guid.NewGuid();

                    db.Insert(item);
                    db.Update(sourceProduct);
                }
                return new HttpResponseMessage(HttpStatusCode.Created)
                {
                    Content = new StringContent("Records added successfully.")
                };
            }
        }

        /// <summary>
        /// Deleting a record information from the database.
        /// </summary>
        /// <param name="id">Record id</param>
        /// <returns>Delete response message</returns>
        internal HttpResponseMessage Delete(int id)
        {
            if (id <= 0)
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Id can't be zero or negative.")
                };

            using (var db = _dbFactory.OpenDbConnection())
            {
                RCD01 order = db.SingleById<RCD01>(id);

                if (order == null)
                    return new HttpResponseMessage(HttpStatusCode.NotFound);

                db.DeleteById<RCD01>(id);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Record deleted successfully.")
                };
            }
        }

        /// <summary>
        /// Getting all record details.
        /// </summary>
        /// <returns>List of records</returns>
        internal List<RCD01> GetAll()
        {
            MySqlConnection _connection = new MySqlConnection("Server=localhost;Port=3306;Database=onlineshopping;User Id=Admin;Password=gs@123;");
            List<RCD01> lstRecords = new List<RCD01>();

            try
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM RCD01;", _connection))
                {
                    _connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lstRecords.Add(new RCD01()
                            {
                                D01F01 = (int)reader[0],
                                D01F02 = (int)reader[1],
                                D01F03 = (int)reader[2],
                                D01F04 = (int)reader[3],
                                D01F05 = (int)reader[4],
                                D01F06 = (Guid)reader[5]
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _connection.Close();
            }

            return lstRecords;
        }

        /// <summary>
        /// Updating record information in database.
        /// </summary>
        /// <param name="objUpdateRecord">Updated information</param>
        /// <returns>Update response message</returns>
        internal HttpResponseMessage Update(RCD01 objUpdateRecord)
        {
            if (objUpdateRecord.D01F01 <= 0)
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Id can't be zero or negative.")
                };

            using (var db = _dbFactory.OpenDbConnection())
            {
                RCD01 existingRecord = db.SingleById<RCD01>(objUpdateRecord.D01F01);

                if (existingRecord == null)
                    return new HttpResponseMessage(HttpStatusCode.NotFound);

                existingRecord.D01F05 = (existingRecord.D01F05 / existingRecord.D01F04) * objUpdateRecord.D01F04;
                existingRecord.D01F04 = objUpdateRecord.D01F04;

                db.Update(existingRecord);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Record updated successfully.")
                };
            }
        }

        /// <summary>
        /// Creating a json respose for view model which contains the information of order detail of specific customer
        /// </summary>
        /// <param name="result">Order detail list</param>
        /// <returns>Json attachment response</returns>
        private HttpResponseMessage JSONResponse(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                SqlExpression<RCD01> joinSql = db.From<RCD01>()
                    .Join<PRO01>((r, p) => r.D01F03 == p.O01F01)
                    .Join<CUS01>((r, c) => r.D01F02 == c.S01F01);

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

                string jsonData = JsonConvert.SerializeObject(result, Formatting.Indented);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

                response.Content = new StringContent(jsonData);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = result[0].CustomerName + " Order Data.json"
                };

                return response;
            }
        }

        /// <summary>
        /// Creating a excel respose for view model which contains the information of order detail of specific customer
        /// </summary>
        /// <param name="result"></param>
        /// <returns>Excel attachment response</returns>
        private HttpResponseMessage ExcelResponse(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                SqlExpression<RCD01> joinSql = db.From<RCD01>()
                    .Join<PRO01>((r, p) => r.D01F03 == p.O01F01)
                    .Join<CUS01>((r, c) => r.D01F02 == c.S01F01);

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


                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("DataSheet");

                    worksheet.Cells["A1"].Value = "Order Id";
                    worksheet.Cells["B1"].Value = "Customer Name";
                    worksheet.Cells["C1"].Value = "Product Name";
                    worksheet.Cells["D1"].Value = "Price";
                    worksheet.Cells["E1"].Value = "Quantity";
                    worksheet.Cells["F1"].Value = "Invoice Id";

                    int row = 2;
                    foreach (var item in result)
                    {
                        worksheet.Cells[row, 1].Value = item.OrderId;
                        worksheet.Cells[row, 2].Value = item.CustomerName;
                        worksheet.Cells[row, 3].Value = item.ProductName;
                        worksheet.Cells[row, 4].Value = item.Price;
                        worksheet.Cells[row, 5].Value = item.Quantity;
                        worksheet.Cells[row, 6].Value = item.InvoiceId;

                        row++;
                    }

                    byte[] content = package.GetAsByteArray();
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ByteArrayContent(content)
                    };

                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = result[0].CustomerName + ".xlsx"
                    };

                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    return response;
                }
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

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}