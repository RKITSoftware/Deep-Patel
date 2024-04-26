using OnlineShoppingAPI.BL.Common.Interface;
using OnlineShoppingAPI.BL.Common.Service;
using OnlineShoppingAPI.BL.Master.Interface;
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

namespace OnlineShoppingAPI.BL.Master.Service
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
            string s01F03;

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                s01F03 = db.SingleById<CUS01>(_objCRT01.T01F02).S01F03;
            }

            return _rcd01Service.BuyCartItems(new List<CRT01>() { _objCRT01 }, s01F03);
        }

        /// <summary>
        /// Checks the cart item if exists or not.
        /// </summary>
        /// <param name="id">Cart item id.</param>
        /// <returns>Ok Response if item exists else NotFound Response</returns>
        public Response BuySingleItemValidation(int id)
        {
            if (IsCRT01Exist(id))
            {
                return OkResponse();
            }

            return NotFoundResponse("Item not found.");
        }

        /// <summary>
        /// Delete the specified cart items from the database.
        /// </summary>
        /// <param name="id">Cart Id.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response Delete(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.Delete(_objCRT01);
            }

            return OkResponse("Item removed successfully.");
        }

        /// <summary>
        /// Checks the item exists or not for delete operation.
        /// </summary>
        /// <param name="id">Cart id</param>
        /// <returns>Ok Response if item exists else NotFound Response.</returns>
        public Response DeleteValidation(int id)
        {
            if (IsCRT01Exist(id))
            {
                return OkResponse();
            }

            return NotFoundResponse("Cart item not found.");
        }

        /// <summary>
        /// Generates a OTP for the 2-Factor Authentication process of buying all items.
        /// </summary>
        /// <param name="id">Customer ID.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GenerateOTP(int id)
        {
            // Generate a random OTP.
            Random random = new Random();
            string otp = random.Next(0, 999999).ToString("000000");

            string s01F03;

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                s01F03 = db.SingleById<CUS01>(id).S01F03;
            }

            _emailService.Send(s01F03, otp);

            ServerCache.Add(
                key: s01F03,
                value: otp,
                dependencies: null,
                absoluteExpiration: DateTime.Now.AddMinutes(5),
                slidingExpiration: TimeSpan.Zero,
                priority: CacheItemPriority.Default,
                onRemoveCallback: null);

            return OkResponse("OTP sent successfully.");
        }

        /// <summary>
        /// Validation checks before the generating otp for the customer.
        /// </summary>
        /// <param name="id">Customer id.</param>
        /// <returns>Ok response if customer exist else notfound response.</returns>
        public Response GenerateOTPValidation(int id)
        {
            bool isCUS01Exist;

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                isCUS01Exist = db.Exists<CUS01>(c => c.S01F01 == id);
            }

            if (isCUS01Exist)
            {
                return OkResponse();
            }

            return NotFoundResponse("Customer not found.");
        }

        /// <summary>
        /// Gets the customer's cart details by using id.
        /// </summary>
        /// <param name="id">Customer Id.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GetCUS01CRT01Details(int id)
        {
            List<CRT01> lstCRT01;

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                lstCRT01 = db.Where<CRT01>("T01F02", id);
            }

            if (lstCRT01 == null)
            {
                return NoContentResponse();
            }

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
            {
                return NoContentResponse();
            }

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
            bool isCUS01Exist;

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                isCUS01Exist = db.Exists<CUS01>(c => c.S01F01 == objDTOCRT01.T01F02);
                _objPRO02 = db.SingleById<PRO02>(_objCRT01.T01F03);
            }

            if (!isCUS01Exist)
            {
                return NotFoundResponse("Customer doesn't exist.");
            }

            if (_objPRO02 == null)
            {
                return NotFoundResponse("Product not found.");
            }

            return OkResponse();
        }

        /// <summary>
        /// Performs the create or update changes to the database.
        /// </summary>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response Save()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                db.Insert(_objCRT01);
            }

            return OkResponse("Item added successfully.");
        }

        /// <summary>
        /// Validates the object for creating or updated related operations.
        /// </summary>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response Validation()
        {
            // Quantity Check
            if (_objPRO02.O02F05 < _objCRT01.T01F04)
            {
                return PreConditionFailedResponse(
                    "Product can't be bought because it can't satisfy quantity.");
            }

            return OkResponse();
        }

        /// <summary>
        /// Verifies the OTP.
        /// </summary>
        /// <param name="id">Customer Id.</param>
        /// <param name="otp">OTP (One Time Password) for the verification of buying process.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response VerifyOTP(int id, string otp)
        {
            string email;

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                email = db.SingleById<CUS01>(id)?.S01F03;
            }

            string existingOTP = ServerCache.Get(email)?.ToString();

            if (existingOTP == null)
            {
                return PreConditionFailedResponse("No OTP is generated for buying or OTP expired.");
            }

            if (!existingOTP.Equals(otp))
            {
                return PreConditionFailedResponse("Incorrect OTP.");
            }

            ServerCache.Remove(email);
            return OkResponse();
        }

        /// <summary>
        /// Buys all items from the customer's cart.
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <returns>Success response.</returns>
        public Response BuyAllItems(int id)
        {
            string s01F03;
            List<CRT01> lstItems;

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                lstItems = db.Where<CRT01>("T01F02", id);
                s01F03 = db.SingleById<CUS01>(id).S01F03;
            }

            if (lstItems == null)
            {
                return PreConditionFailedResponse("No cart items for customer.");
            }

            return _rcd01Service.BuyCartItems(lstItems, s01F03);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Checks the cart item exists or not.
        /// </summary>
        /// <param name="id">Cart id.</param>
        /// <returns>True if exists else false.</returns>
        private bool IsCRT01Exist(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Exists<CRT01>(c => c.T01F01 == id);
            }
        }

        #endregion Private Methods
    }
}