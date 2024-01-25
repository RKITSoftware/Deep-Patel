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
        private static readonly IDbConnectionFactory _dbFactory;

        static BLRecord()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        public static HttpResponseMessage Create(RCD01 objRecord)
        {
            if (objRecord == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            using (var db = _dbFactory.OpenDbConnection())
            {
                bool tableExist = db.TableExists<RCD01>();

                if (!tableExist)
                    db.CreateTable<RCD01>();

                var sourceProduct = db.SingleById<PRO01>(objRecord.D01F03);

                if (sourceProduct != null)
                {
                    objRecord.D01F05 = sourceProduct.O01F03 * objRecord.D01F04;
                    objRecord.D01F06 = Guid.NewGuid();
                }

                db.Insert<RCD01>(objRecord);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Record added successfully")
                };
            }
        }

        public static HttpResponseMessage CreateFromList(List<RCD01> lstNewRecords)
        {
            if (lstNewRecords.Count == 0)
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Data is empty")
                };

            using (var db = _dbFactory.OpenDbConnection())
            {
                bool tableExists = db.TableExists<RCD01>();

                if (!tableExists)
                    db.CreateTable<RCD01>();

                db.InsertAll<RCD01>(lstNewRecords);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Records added successfully.")
                };
            }
        }

        public static HttpResponseMessage Delete(int id)
        {
            if (id <= 0)
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Id can't be zero or negative.")
                };

            using (var db = _dbFactory.OpenDbConnection())
            {
                var order = db.SingleById<RCD01>(id);

                if (order == null)
                    return new HttpResponseMessage(HttpStatusCode.NotFound);

                db.DeleteById<RCD01>(id);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Record deleted successfully.")
                };
            }
        }

        public static List<RCD01> GetAll()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                bool tableExists = db.TableExists<RCD01>();

                if (!tableExists)
                    return null;

                return db.Select<RCD01>();
            }
        }

        public static HttpResponseMessage Update(RCD01 objUpdateRecord)
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

        private static List<OrderDetailViewModel> GetOrderDetail(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var joinSql = db.From<RCD01>()
                    .Join<PRO01>((r, p) => r.D01F03 == p.O01F01)
                    .Join<CUS01>((r, c) => r.D01F02 == c.S01F01);

                var result = db.SelectMulti<RCD01, PRO01, CUS01>(joinSql)
                    .Where((r) => r.Item1.D01F02 == id)
                    .Select((r) => new OrderDetailViewModel
                    {
                        OrderId = r.Item1.D01F01,
                        CustomerName = r.Item3.S01F02,
                        ProductName = r.Item2.O01F02,
                        Price = r.Item1.D01F05,
                        Quantity = r.Item1.D01F04,
                        InvoiceId = r.Item1.D01F06
                    })
                    .ToList();

                return result;
            }
        }

        private static HttpResponseMessage JSONResponse(List<OrderDetailViewModel> result)
        {
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

        private static HttpResponseMessage ExcelResponse(List<OrderDetailViewModel> result)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("DataSheet");

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

                var content = package.GetAsByteArray();
                var response = new HttpResponseMessage(HttpStatusCode.OK)
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

        public static HttpResponseMessage Download(int id, string filetype)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var result = GetOrderDetail(id);

                if (result == null)
                    return new HttpResponseMessage(HttpStatusCode.NotFound);

                if(filetype.Equals("Json"))
            
                    return JSONResponse(result);
                
                else if(filetype.Equals("Excel"))
                {
                    return ExcelResponse(result);
                }
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}