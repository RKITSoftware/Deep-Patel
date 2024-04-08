using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.Business_Logic;
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
using System.Net;
using System.Web;

namespace OnlineShoppingAPI.BL.Service
{
    public class BLPRO02 : IPRO02Service
    {
        private readonly IDbConnectionFactory _dbFactory;
        private EnmOperation _operation;
        private PRO02 _objPRO02;
        private readonly DBPRO02 _dbPRO02;

        public BLPRO02()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            _dbPRO02 = new DBPRO02();
        }

        public void PreSave(DTOPRO02 objPRO02DTO, EnmOperation operation)
        {
            _operation = operation;
            _objPRO02 = objPRO02DTO.Convert<PRO02>();
            _objPRO02.O02F08 = DateTime.Now;

            if (_objPRO02.O02F03 >= 0 && _objPRO02.O02F04 >= 0 &&
                        _objPRO02.O02F05 >= 0)
            {
                _objPRO02.O02F07 = (int)EnmProductStatus.InStock;
            }
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

        private void Create(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.Insert(_objPRO02);

                    response = new Response()
                    {
                        StatusCode = HttpStatusCode.Created,
                        Message = "Product successfully created."
                    };
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
            throw new NotImplementedException();
        }

        public void Delete(int id, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    bool isExist = db.Exists<PRO02>(p => p.O02F01 == id);

                    if (!isExist)
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Product not found."
                        };
                        return;
                    }

                    db.DeleteById<PRO02>(id);
                    response = BLHelper.OkResponse();
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
                    List<PRO02> lstPRO02 = db.Select<PRO02>();

                    if (lstPRO02 == null || lstPRO02.Count == 0)
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.NoContent,
                            Message = "Product data doesn't available."
                        };
                    }
                    else
                    {
                        response = BLHelper.OkResponse();
                        response.Data = lstPRO02;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = BLHelper.ISEResponse();
            }
        }

        public void UpdateSellPrice(int id, int sellPrice, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    PRO02 existingProduct = db.SingleById<PRO02>(id);

                    if (existingProduct == null)
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Product not found."
                        };
                    }
                    else
                    {
                        existingProduct.O02F04 = sellPrice;
                        db.Update(existingProduct);

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

        public void GetInformation(out Response response)
        {
            _dbPRO02.GetInformation(out response);
        }
    }
}