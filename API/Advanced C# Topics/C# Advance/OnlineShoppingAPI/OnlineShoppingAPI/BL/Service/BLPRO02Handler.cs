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
using static OnlineShoppingAPI.BL.Common.BLHelper;

namespace OnlineShoppingAPI.BL.Service
{
    /// <summary>
    /// Service implementation of <see cref="IPRO02Service"/>.
    /// </summary>
    public class BLPRO02Handler : IPRO02Service
    {
        #region Private Fields

        /// <summary>
        /// Orm Lite Connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Databse Context of <see cref="DBPRO02Context"/> for MySQL Queries.
        /// </summary>
        private readonly DBPRO02Context _dbPRO02Context;

        /// <summary>
        /// Instance of <see cref="PRO02"/> for create or update related operations.
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
        /// Initialize the instance when it's created.
        /// </summary>
        public BLPRO02Handler()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            _dbPRO02Context = new DBPRO02Context();
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Deletes the product specified by id.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response Delete(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                if (!db.Exists<PRO02>(p => p.O02F01 == id))
                {
                    return NotFoundResponse("Product not found.");
                }

                db.DeleteById<PRO02>(id);
            }

            return OkResponse("Product deleted successfully.");
        }

        /// <summary>
        /// Retrieves all products information from the database.
        /// </summary>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GetAll()
        {
            List<PRO02> lstPRO02;

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                lstPRO02 = db.Select<PRO02>();
            }

            if (lstPRO02 == null)
            {
                return NoContentResponse();
            }

            return OkResponse("", lstPRO02);
        }

        /// <summary>
        /// Gets the product's full information using DB.
        /// </summary>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GetInformation()
        {
            DataTable dtInfo = _dbPRO02Context.GetInformation();

            if (dtInfo.Rows.Count == 0)
            {
                return NoContentResponse();
            }

            return OkResponse("", dtInfo);
        }

        /// <summary>
        /// Performs the Conversion operation and prepares the objects for create or update.
        /// </summary>
        /// <param name="objPRO02DTO">DTO of PRO02.</param>
        public void PreSave(DTOPRO02 objPRO02DTO)
        {
            _objPRO02 = objPRO02DTO.Convert<PRO02>();
            _objPRO02.O02F08 = DateTime.Now;

            if (_objPRO02.O02F03 >= 0 && _objPRO02.O02F04 >= 0 &&
                        _objPRO02.O02F05 >= 0)
            {
                _objPRO02.O02F07 = (int)EnmProductStatus.InStock;
            }
        }

        /// <summary>
        /// Checks the record exists or not for operation.
        /// </summary>
        /// <param name="objDTOPRO02">DTO containing the Product information.</param>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response PreValidation(DTOPRO02 objDTOPRO02)
        {
            if (Operation == EnmOperation.A)
            {
                if (objDTOPRO02.O02F01 != 0)
                {
                    return PreConditionFailedResponse("Id needs to be zero for add operation");
                }
            }
            else
            {
                if (objDTOPRO02.O02F01 <= 0)
                {
                    return PreConditionFailedResponse("Id needs to greater than zero for edit operation.");
                }
            }

            using (var db = _dbFactory.OpenDbConnection())
            {
                // Checks category exists or not.
                if (db.SingleById<CAT01>(objDTOPRO02.O02F09) == null)
                {
                    return NotFoundResponse("Category doesn't exist.");
                }

                // Checks supplier exists or not.
                if (db.SingleById<SUP01>(objDTOPRO02.O02F10) == null)
                {
                    return NotFoundResponse("Supplier doesn't exist.");
                }
            }

            return OkResponse();
        }

        /// <summary>
        /// Save the object information to the database according to opertaion.
        /// </summary>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response Save()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.Insert(_objPRO02);
            }

            return OkResponse("Product created successfully.");
        }

        /// <summary>
        /// Updates the sell price of the specified product which id is given.
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="sellPrice">Updates sell price.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response UpdateSellPrice(int id, int sellPrice)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                _objPRO02 = db.SingleById<PRO02>(id);

                if (_objPRO02 == null)
                {
                    return NotFoundResponse("Product not found.");
                }

                _objPRO02.O02F04 = sellPrice;
                db.Update(_objPRO02);
            }

            return OkResponse("Product sell price updated successfully.");
        }

        /// <summary>
        /// Validates the objects before the save process.
        /// </summary>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response Validation()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                if (db.Exists<PRO02>(p => p.O02F02 == _objPRO02.O02F02
                    && p.O02F09 == _objPRO02.O02F09
                    && p.O02F10 == _objPRO02.O02F10))
                {
                    return PreConditionFailedResponse("Product can't be created because it already exists.");
                }
            }

            return OkResponse();
        }

        #endregion Public Methods
    }
}