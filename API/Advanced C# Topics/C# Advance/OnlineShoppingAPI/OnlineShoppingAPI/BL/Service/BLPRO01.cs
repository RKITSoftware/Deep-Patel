using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.Business_Logic;
using OnlineShoppingAPI.Extension;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;
using OnlineShoppingAPI.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;

namespace OnlineShoppingAPI.BL.Service
{
    public class BLPRO01 : IPRO01Service
    {
        /// <summary>
        /// _dbFactory is used to store the reference of database connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;
        private PRO01 _objPRO01;
        private EnmOperation _operation;

        public BLPRO01()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
        }

        public void PreSave(DTOPRO01 objPRO01DTO, EnmOperation operation)
        {
            _objPRO01 = objPRO01DTO.Convert<PRO01>();
            _operation = operation;
        }

        public bool Validation(out Response response)
        {
            response = null;
            return true;
        }

        public void Save(out Response response)
        {
            if (_operation == EnmOperation.Create)
                Create(out response);
            else
                Update(out response);
        }

        public void Delete(int id, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    PRO01 product = db.SingleById<PRO01>(id);
                    if (product == null)
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Product Not Found."
                        };
                    }
                    else
                    {
                        db.DeleteById<PRO01>(id);
                        response = BLHelper.OkResponse();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = BLHelper.ISEResponse();
            }
        }

        public void GetAll(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    List<PRO01> lstPRO01 = db.Select<PRO01>();

                    response = BLHelper.OkResponse();
                    response.Data = lstPRO01;
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = BLHelper.ISEResponse();
            }
        }

        private void Update(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    PRO01 existingProduct = db.SingleById<PRO01>(_objPRO01.O01F01);

                    if (existingProduct == null)
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.BadRequest,
                            Message = "Product id doesn't exist."
                        };
                        return;
                    }

                    // Update product properties
                    existingProduct.O01F02 = _objPRO01.O01F02;
                    existingProduct.O01F03 = _objPRO01.O01F03;
                    existingProduct.O01F04 = _objPRO01.O01F04;
                    existingProduct.O01F05 = _objPRO01.O01F05;

                    // Perform the database update
                    db.Update(existingProduct);

                    response = BLHelper.OkResponse();
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = BLHelper.ISEResponse();
            }
        }

        private void Create(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.Insert(_objPRO01);

                    response = new Response()
                    {
                        StatusCode = HttpStatusCode.Created,
                        Message = "Product Successfully created."
                    };
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = BLHelper.ISEResponse();
            }
        }

        public void UpdateQuantity(int id, int quantity, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    PRO01 objProduct = db.SingleById<PRO01>(id);

                    if (objProduct == null)
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.BadRequest,
                            Message = "Id doesn't reference to product."
                        };
                        return;
                    }

                    // Update product quantity
                    objProduct.O01F04 += quantity;
                    db.Update(objProduct);

                    response = BLHelper.OkResponse();
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = BLHelper.ISEResponse();
            }
        }
    }
}