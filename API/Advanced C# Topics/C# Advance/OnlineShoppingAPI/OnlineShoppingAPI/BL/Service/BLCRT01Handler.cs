using OnlineShoppingAPI.BL.Common.Interface;
using OnlineShoppingAPI.BL.Common.Service;
using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.DL;
using OnlineShoppingAPI.Extension;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Caching;
using static OnlineShoppingAPI.BL.Common.BLHelper;

namespace OnlineShoppingAPI.BL.Service
{
    /// <summary>
    /// Service implementation of <see cref="ICRT01Service"/>.
    /// </summary>
    public class BLCRT01Handler : ICRT01Service
    {
        #region Private Fields

        /// <summary>
        /// Db Context of <see cref="DBCRT01Context"/> for performing my sql queries.
        /// </summary>
        private readonly DBCRT01Context _dbCRT01Context;

        /// <summary>
        /// OrmLite connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Email services to send the emails.
        /// </summary>
        private readonly IEmailService _emailService;

        /// <summary>
        /// <see cref="IRCD01Service"/> for handling record related operations.
        /// </summary>
        private readonly IRCD01Service _rcd01Service;

        /// <summary>
        /// <see cref="CRT01"/> object for creating or updating data.
        /// </summary>
        private CRT01 _objCRT01;

        /// <summary>
        /// Instance of <see cref="PRO02"/>.
        /// </summary>
        private PRO02 _objPRO02;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Specifies the operation to perform.
        /// </summary>
        public EnmOperation Operation { get; set; }

        #endregion Public Properties

        #region Constructor

        /// <summary>
        /// Initialize ths instance of <see cref="BLCRT01Handler"/>.
        /// </summary>
        public BLCRT01Handler()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            _emailService = new BLEmail();
            _dbCRT01Context = new DBCRT01Context();
            _rcd01Service = new BLRCD01Handler();
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// For buying one single item from the cart.
        /// </summary>
        /// <param name="id">Cart id</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response BuySingleItem(int id)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    CRT01 objCart = db.SingleById<CRT01>(id);

                    if (objCart == null)
                        return NotFoundResponse("Item not found.");

                    CUS01 objCustomer = db.SingleById<CUS01>(objCart.T01F02);

                    List<CRT01> lstCart = new List<CRT01>()
                    {
                        objCart
                    };

                    return _rcd01Service.BuyCartItems(lstCart, objCustomer.S01F03);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Delete the specified cart items from the database.
        /// </summary>
        /// <param name="id">Cart Id.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response Delete(int id)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    CRT01 objItem = db.SingleById<CRT01>(id);

                    if (objItem == null)
                        return NotFoundResponse("Item not found.");

                    db.Delete(objItem);
                }
            }
            catch (Exception ex) { throw ex; }

            return OkResponse("Item removed successfully.");
        }

        /// <summary>
        /// Generates a OTP for the 2-Factor Authentication process of buying all items.
        /// </summary>
        /// <param name="id">Customer ID.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response Generate(int id)
        {
            // Generate a random OTP.
            Random random = new Random();
            string otp = random.Next(0, 999999).ToString("000000");

            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve the customer's email address from the database.
                    string email = db.SingleById<CUS01>(id)?.S01F03;

                    // If the customer's email is not found, return NotFound response.
                    if (string.IsNullOrEmpty(email))
                        return NotFoundResponse("Customer not found");

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
            }
            catch (Exception ex) { throw ex; }

            return OkResponse("OTP sent successfully.");
        }

        /// <summary>
        /// Gets the customer's cart details by using id.
        /// </summary>
        /// <param name="id">Customer Id.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GetCUS01CRT01Details(int id)
        {
            List<CRT01> lstCRT01;

            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                    lstCRT01 = db.Where<CRT01>("T01F02", id);
            }
            catch (Exception ex) { throw ex; }

            if (lstCRT01 == null || lstCRT01.Count == 0)
                return NoContentResponse();

            return OkResponse("", lstCRT01);
        }

        /// <summary>
        /// Gets the full infomration of customer's cart items with product name.
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GetFullCRT01InfoOfCUS01(int id)
        {
            DataTable dtData = _dbCRT01Context.GetFullDetailsOfCart(id);

            if (dtData.Rows.Count == 0)
                return NoContentResponse();

            return OkResponse("", dtData);
        }

        /// <summary>
        /// Initialize objects for the creating or updating related operations.
        /// </summary>
        /// <param name="objDTOCRT01">DTO for CRT01.</param>
        public void PreSave(DTOCRT01 objDTOCRT01)
        {
            _objCRT01 = objDTOCRT01.Convert<CRT01>();
            _objCRT01.T01F05 = _objPRO02.O02F04;
        }

        /// <summary>
        /// Validation checks for the primary key's and foreign key of different tables.
        /// </summary>
        /// <param name="objDTOCRT01">DTO object of CRT model.</param>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response PreValidation(DTOCRT01 objDTOCRT01)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Customer exists or not.
                    if (db.SingleById<CUS01>(objDTOCRT01.T01F02) == null)
                        return NotFoundResponse("Customer doesn't exist.");

                    // Product exists or not.
                    _objPRO02 = db.SingleById<PRO02>(_objCRT01.T01F03);

                    if (_objPRO02 == null)
                        return NotFoundResponse("Product doesn't exist.");
                }
            }
            catch (Exception ex) { throw ex; }

            return OkResponse();
        }

        /// <summary>
        /// Performs the create or update changes to the database.
        /// </summary>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response Save()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                    db.Insert(_objCRT01);
            }
            catch (Exception ex) { throw ex; }

            return OkResponse("Item added successfully.");
        }

        /// <summary>
        /// Validates the object for creating or updated related operations.
        /// </summary>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response Validation()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Quantity Check
                    if (_objPRO02.O02F05 < _objCRT01.T01F04)
                        return PreConditionFailedResponse("Product can't be bought because it can't satisfy quantity.");
                }
            }
            catch (Exception ex) { throw ex; }

            return OkResponse();
        }

        /// <summary>
        /// Verifies the OTP and Buy items from the cart of customer's.
        /// </summary>
        /// <param name="id">Customer Id.</param>
        /// <param name="otp">OTP (One Time Password) for the verification of buying process.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response VerifyAndBuy(int id, string otp)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    string email = db.SingleById<CUS01>(id)?.S01F03;
                    string existingOTP = ServerCache.Get(email)?.ToString();

                    // If no OTP is generated for buying items, return NotFound response.
                    if (existingOTP == null)
                        return PreConditionFailedResponse("No OTP is generated for buying or OTP expired.");

                    // Check if the provided OTP matches the existing OTP for verification.
                    if (!existingOTP.Equals(otp))
                        return PreConditionFailedResponse("Incorrect OTP.");

                    ServerCache.Remove(email);
                }
            }
            catch (Exception ex) { throw ex; }

            return BuyAllItems(id);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Buy all items of the customer after successful completion 2-Factor Authentication process.
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        private Response BuyAllItems(int id)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    List<CRT01> lstItems = db.Where<CRT01>("T01F02", id);

                    if (lstItems == null)
                        return PreConditionFailedResponse("No cart items for customer.");

                    CUS01 objCustomer = db.SingleById<CUS01>(id);
                    return _rcd01Service.BuyCartItems(lstItems, objCustomer.S01F03);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion Private Methods
    }
}