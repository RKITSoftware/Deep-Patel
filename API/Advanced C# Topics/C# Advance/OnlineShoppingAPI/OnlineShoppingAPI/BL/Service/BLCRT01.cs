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
using System.Net;
using System.Web;
using System.Web.Caching;
using static OnlineShoppingAPI.BL.Common.BLHelper;

namespace OnlineShoppingAPI.BL.Service
{
    /// <summary>
    /// Service implementation of <see cref="ICRT01Service"/>.
    /// </summary>
    public class BLCRT01 : ICRT01Service
    {
        #region Private Fields

        /// <summary>
        /// OrmLite connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// <see cref="CRT01"/> object for creating or updating data.
        /// </summary>
        private CRT01 _objCRT01;

        /// <summary>
        /// Instance of <see cref="PRO02"/>.
        /// </summary>
        private PRO02 _objPRO02;

        /// <summary>
        /// Email services to send the emails.
        /// </summary>
        private readonly IEmailService _emailService;

        /// <summary>
        /// <see cref="IRCD01Service"/> for handling record related operations.
        /// </summary>
        private readonly IRCD01Service _rcd01Service;

        /// <summary>
        /// Db Context of <see cref="DBCRT01"/> for performing my sql queries.
        /// </summary>
        private readonly DBCRT01 _dbCRT01;

        #endregion

        #region Public Properties

        /// <summary>
        /// Specifies the operation to perform.
        /// </summary>
        public EnmOperation Operation { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize ths instance of <see cref="BLCRT01"/>.
        /// </summary>
        public BLCRT01()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            _emailService = new BLEmail();
            _dbCRT01 = new DBCRT01();
            _rcd01Service = new BLRCD01();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Validation checks for the primary key's and foreign key of different tables.
        /// </summary>
        /// <param name="objDTOCRT01"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public bool PreValidation(DTOCRT01 objDTOCRT01, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Customer exists or not.
                    if (!db.Exists<CUS01>(c => c.S01F01 == objDTOCRT01.T01102))
                    {
                        response = NotFoundResponse("Customer doesn't exist.");
                        return false;
                    }

                    // Product exists or not.
                    _objPRO02 = db.SingleById<PRO02>(_objCRT01.T01F03);
                    if (_objPRO02 == null)
                    {
                        response = NotFoundResponse("Product doesn't exist.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }

            response = null;
            return true;
        }

        /// <summary>
        /// Initialize objects for the creating or updating related operations.
        /// </summary>
        /// <param name="objDTOCRT01">DTO for CRT01.</param>
        public void PreSave(DTOCRT01 objDTOCRT01)
        {
            _objCRT01 = objDTOCRT01.Convert<CRT01>();
        }

        /// <summary>
        /// Performs the create or update changes to the database.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void Save(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    _objCRT01.T01F05 = _objPRO02.O02F04;
                    db.Insert(_objCRT01);
                }

                response = OkResponse("Item added successfully.");
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
        }

        /// <summary>
        /// Validates the object for creating or updated related operations.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        /// <returns><see langword="true"/> if validation successful, else <see langword="false"/>.</returns>
        public bool Validation(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Quantity Check
                    if (_objPRO02.O02F05 < _objCRT01.T01F04)
                    {
                        response = PreConditionFailedResponse("Product can't be bought because it can't satisfy quantity.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
                return false;
            }

            response = null;
            return true;
        }

        /// <summary>
        /// Gets the customer's cart details by using id.
        /// </summary>
        /// <param name="id">Customer Id.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void GetCUS01CRT01Details(int id, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    List<CRT01> lstCRT01 = db.Where<CRT01>("T01F02", id);

                    if (lstCRT01 == null || lstCRT01.Count == 0)
                    {
                        response = new Response()
                        {
                            StatusCode = HttpStatusCode.NoContent,
                            Message = "No data available."
                        };
                    }
                    else
                    {
                        response = OkResponse("Success");
                        response.Data = lstCRT01;
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
        /// Delete the specified cart items from the database.
        /// </summary>
        /// <param name="id">Cart Id.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void Delete(int id, out Response response)
        {
            try
            {

                using (var db = _dbFactory.OpenDbConnection())
                {
                    CRT01 objItem = db.SingleById<CRT01>(id);

                    if (objItem == null)
                    {
                        response = NotFoundResponse("Item not found.");
                        return;
                    }

                    db.DeleteById<CRT01>(id);
                    response = OkResponse("Item removed successfully.");
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
        }

        /// <summary>
        /// Generates a OTP for the 2-Factor Authentication process of buying all items.
        /// </summary>
        /// <param name="id">Customer ID.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void Generate(int id, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Generate a random OTP.
                    Random random = new Random();
                    string otp = random.Next(0, 999999).ToString("000000");

                    // Retrieve the customer's email address from the database.
                    string email = db.SingleById<CUS01>(id)?.S01F03;

                    // If the customer's email is not found, return NotFound response.
                    if (string.IsNullOrEmpty(email))
                    {
                        response = NotFoundResponse("Customer not found");
                        return;
                    }

                    // Send the OTP to the customer's registered email.
                    _emailService.Send(email, otp);

                    // Cache the OTP with the customer's email as the key for future validation.
                    ServerCache.Add(
                        key: email,
                        value: otp,
                        dependencies: null,
                        absoluteExpiration: DateTime.Now.AddMinutes(5),
                        slidingExpiration: TimeSpan.Zero,
                        priority: CacheItemPriority.Default,
                        onRemoveCallback: null);
                }

                response = OkResponse("OTP sent successfully.");
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
        }

        /// <summary>
        /// Verifies the OTP and Buy items from the cart of customer's.
        /// </summary>
        /// <param name="id">Customer Id.</param>
        /// <param name="otp">OTP (One Time Password) for the verification of buying process.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void VerifyAndBuy(int id, string otp, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    string email = db.SingleById<CUS01>(id)?.S01F03;
                    string existingOTP = BLHelper.ServerCache.Get(email)?.ToString();

                    // If no OTP is generated for buying items, return NotFound response.
                    if (existingOTP == null)
                    {
                        response = PreConditionFailedResponse("No OTP is generated for buying or OTP expired.");
                        return;
                    }

                    // Check if the provided OTP matches the existing OTP for verification.
                    if (!existingOTP.Equals(otp))
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.BadRequest,
                            Message = "Incorrect OTP."
                        };
                        return;
                    }

                    ServerCache.Remove(email);
                    if (BuyAllItems(id, out response))
                    {
                        response = OkResponse("Items bought successfully.");
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
        /// Gets the full infomration of customer's cart items with product name.
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void GetFullCRT01InfoOfCUS01(int id, out Response response)
            => _dbCRT01.GetFullDetailsOfCart(id, out response);

        /// <summary>
        /// For buying one single item from the cart.
        /// </summary>
        /// <param name="id">Cart id</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void BuySingleItem(int id, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    CRT01 objCart = db.SingleById<CRT01>(id);

                    if (objCart == null)
                    {
                        response = NotFoundResponse("Item not found.");
                        return;
                    }

                    CUS01 objCustomer = db.SingleById<CUS01>(objCart.T01F02);

                    List<CRT01> lstCart = new List<CRT01>()
                    {
                        objCart
                    };

                    _rcd01Service.BuyCartItems(lstCart, objCustomer.S01F03, out response);
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Buy all items of the customer after successful completion 2-Factor Authentication process.
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        /// <returns><see langword="true"/> if items bought successfully without internal error,
        /// else <see langword="false"/>.</returns>
        private bool BuyAllItems(int id, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    List<CRT01> lstItems = db.Where<CRT01>("T01F02", id);

                    if (lstItems == null)
                    {
                        response = NotFoundResponse("No cart items for customer.");
                        return false;
                    }

                    CUS01 objCustomer = db.SingleById<CUS01>(id);
                    return _rcd01Service.BuyCartItems(lstItems, objCustomer.S01F03, out response);
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
                return false;
            }
        }

        #endregion
    }
}