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
using static OnlineShoppingAPI.BL.Common.BLHelper;

namespace OnlineShoppingAPI.BL.Master.Service
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

        /// <summary>
        /// List containing the product version 2 model data.
        /// </summary>
        private List<PRO02> _lstPRO02;

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
                db.DeleteById<PRO02>(id);
            }

            return OkResponse("Product deleted successfully.");
        }

        /// <summary>
        /// Validation checks before the delete operation.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response DeleteValidation(int id)
        {
            bool isPRO02Exist;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                isPRO02Exist = db.Exists<PRO02>(p => p.O02F01 == id);
            }

            return !isPRO02Exist ? NotFoundResponse("Product not found.") : OkResponse("Product deleted successfully.");
        }

        /// <summary>
        /// Retrieves all products information from the database.
        /// </summary>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GetAll()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                _lstPRO02 = db.Select<PRO02>();
            }

            return _lstPRO02 == null ? NoContentResponse() : OkResponse("", _lstPRO02);
        }

        /// <summary>
        /// Gets the product's full information using DB.
        /// </summary>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GetInformation()
        {
            DataTable dtInfo = _dbPRO02Context.GetInformation();

            return dtInfo.Rows.Count != 0 ? OkResponse("", dtInfo) : NoContentResponse();
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
            if (IsCAT01Exist(objDTOPRO02.O02F09))
            {
                if (!IsSUP01Exist(objDTOPRO02.O02F10))
                {
                    return NotFoundResponse("Supplier doesn't exist.");
                }

                return OkResponse();
            }

            return NotFoundResponse("Category doesn't exist.");
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
            _objPRO02.O02F04 = sellPrice;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.Update(_objPRO02);
            }

            return OkResponse("Product sell price updated successfully.");
        }

        /// <summary>
        /// Validation checks the product before updating the sell price.
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="sellPrice">Updates sell price.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response UpdateSellPriceValidation(int id, int sellPrice)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                _objPRO02 = db.SingleById<PRO02>(id);
            }

            return _objPRO02 != null ? OkResponse() : NotFoundResponse("Product not found.");
        }

        /// <summary>
        /// Validates the objects before the save process.
        /// </summary>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response Validation()
        {
            bool isDuplicate = false;

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                isDuplicate = db.Exists<PRO02>(p => p.O02F02 == _objPRO02.O02F02
                    && p.O02F09 == _objPRO02.O02F09 && p.O02F10 == _objPRO02.O02F10);
            }

            return isDuplicate ? PreConditionFailedResponse("Product can't be created because it already exists.") : OkResponse();
        }

        /// <summary>
        /// Validate the id before getting the product data of that category.
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns>Success response if category exists else notfound response.</returns>
        public Response ValidationForGetPRO02ByCAT01(int id)
        {
            return IsCAT01Exist(id) ? OkResponse() : NotFoundResponse("Category not found.");
        }

        /// <summary>
        /// Gets the product of category specified by id.
        /// </summary>
        /// <param name="id">Category Id.</param>
        /// <returns>Ok response containing the data if data exists else no content response.</returns>
        public Response GetProductByCategory(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                _lstPRO02 = db.Select<PRO02>(p => p.O02F09 == id);
            }

            return _lstPRO02 == null || _lstPRO02.Count == 0 ? NoContentResponse() : OkResponse("Success", _lstPRO02);
        }

        /// <summary>
        /// Validate the id before getting the product data of that supplier.
        /// </summary>
        /// <param name="id">Supplier Id</param>
        /// <returns>Success response if category exists else notfound response.</returns>
        public Response ValidationForGetPRO02BySUP01(int id)
        {
            return IsSUP01Exist(id) ? OkResponse() : NotFoundResponse("Supplier doesn't exist.");
        }

        /// <summary>
        /// Gets the product of supplier specified by id.
        /// </summary>
        /// <param name="id">Supplier Id.</param>
        /// <returns>Ok response containing the data if data exists else no content response.</returns>
        public Response GetProductBySupplier(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                _lstPRO02 = db.Select<PRO02>(p => p.O02F10 == id);
            }

            return _lstPRO02 != null && _lstPRO02.Count != 0 ? OkResponse("", _lstPRO02) : NoContentResponse();
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Checks the supplier is exists or not.
        /// </summary>
        /// <param name="id">Supplier id.</param>
        /// <returns>True is exists else False.</returns>
        private bool IsSUP01Exist(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Exists<SUP01>(s => s.P01F01 == id);
            }
        }

        /// <summary>
        /// Checks the category is exists or not.
        /// </summary>
        /// <param name="id">Category id.</param>
        /// <returns>True is exists else False.</returns>
        /// <exception cref="NotImplementedException"></exception>
        private bool IsCAT01Exist(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Exists<CAT01>(c => c.T01F01 == id);
            }
        }

        #endregion
    }
}