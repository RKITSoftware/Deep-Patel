using OnlineShoppingAPI.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;

namespace OnlineShoppingAPI.Business_Logic
{
    public class BLCart
    {
        private static readonly IDbConnectionFactory _dbFactory;
        private static string _logFolderPath;

        static BLCart()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            _logFolderPath = HttpContext.Current.Application["LogFolderPath"] as string;

            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        internal HttpResponseMessage Add(CRT01 objProduct)
        {
            try
            {
                if (objProduct == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.PreconditionFailed)
                    {
                        Content = new StringContent("Product data is null.")
                    };
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    PRO01 sourceProduct = db.Single(db.From<PRO01>().Where(product => product.O01F01 == objProduct.T01F03));

                    if (sourceProduct == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound);
                    }

                    if (sourceProduct.O01F04 < objProduct.T01F04)
                    {
                        return new HttpResponseMessage(HttpStatusCode.PreconditionFailed)
                        {
                            Content = new StringContent("Product can't be bought because the quantity can't be satisfied.")
                        };
                    }

                    objProduct.T01F05 = sourceProduct.O01F03 * objProduct.T01F04;
                    db.Insert(objProduct);

                    return new HttpResponseMessage(HttpStatusCode.Created)
                    {
                        Content = new StringContent("Product added successfully to cart.")
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLException.SendErrorToTxt(ex, _logFolderPath);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while adding item to cart.")
                };
            }
        }

        internal HttpResponseMessage Delete(int cartId)
        {
            try
            {
                if (cartId <= 0)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Id can't be negeative nor zero")
                    };
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    CRT01 objItem = db.SingleById<CRT01>(cartId);

                    if (objItem == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent("Item can't found.")
                        };
                    }

                    db.DeleteById<CRT01>(cartId);
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Item deleted successfully from your cart.")
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLException.SendErrorToTxt(ex, _logFolderPath);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while deleting item from your cart.")
                };
            }
        }

        internal List<CRT01> Get(int customerId)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (!db.TableExists<CRT01>())
                    {
                        db.CreateTable<CRT01>();
                    }

                    return db.Where<CRT01>("T01F02", customerId) ?? new List<CRT01>();
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLException.SendErrorToTxt(ex, _logFolderPath);
                return null;
            }
        }
    }
}