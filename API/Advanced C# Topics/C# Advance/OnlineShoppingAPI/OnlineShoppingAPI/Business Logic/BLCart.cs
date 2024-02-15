using OnlineShoppingAPI.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Caching;

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

        internal HttpResponseMessage BuyAllItems(int customerId)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    List<CRT01> lstItems = db.Where<CRT01>("T01F02", customerId);

                    if (lstItems == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent("Customer have nothing in his cart.")
                        };
                    }

                    BLRecord objRecord = new BLRecord();
                    return objRecord.AddRecords(lstItems);
                };

            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLException.SendErrorToTxt(ex, _logFolderPath);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while buying all items of cart.")
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

        internal HttpResponseMessage Generate(int customerId)
        {
            try
            {
                if (customerId <= 0)
                {
                    return new HttpResponseMessage(HttpStatusCode.PreconditionFailed)
                    {
                        Content = new StringContent("Customer Id can't be null.")
                    };
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    Random random = new Random();
                    string otp = random.Next(0, 999999).ToString("000000");
                    string email = db.SingleById<CUS01>(customerId).S01F03;

                    SendEmail(email, otp);
                    BLHelper.ServerCache.Add(
                        key: email,
                        value: otp,
                        dependencies: null,
                        absoluteExpiration: DateTime.Now.AddMinutes(5),
                        slidingExpiration: TimeSpan.Zero,
                        priority: CacheItemPriority.Default,
                        onRemoveCallback: null);
                }

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Otp send to your registered email")
                };
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

        private void SendEmail(string email, string otp)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587);

            smtpClient.Credentials = new NetworkCredential("deeppatel2513@outlook.com", "@Deep2513");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("deeppatel2513@outlook.com", "Deep Patel");
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = "OTP for Buying";
            mailMessage.Body = $"Otp for buying items which are in your cart is :- {otp}";

            smtpClient.Send(mailMessage);
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

        internal HttpResponseMessage VerifyAndBuy(int customerId, string otp)
        {
            try
            {
                if (customerId <= 0)
                {
                    return new HttpResponseMessage(HttpStatusCode.PreconditionFailed)
                    {
                        Content = new StringContent("Id can't be negative nor zero.")
                    };
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    string email = db.SingleById<CUS01>(customerId).S01F03;
                    string existingOTP = BLHelper.ServerCache.Get(email)?.ToString();

                    if (existingOTP == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent("Otp isn't generated for buing items.")
                        };
                    }

                    if (!existingOTP.Equals(otp))
                    {
                        return new HttpResponseMessage(HttpStatusCode.BadRequest)
                        {
                            Content = new StringContent("Otp is incorrect.")
                        };
                    }

                    BuyAllItems(customerId);
                    BLCaching.ServerCache.Remove(email);

                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Items bought successfully.")
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLException.SendErrorToTxt(ex, _logFolderPath);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while verifying otp for buying.")
                };
            }
        }
    }
}