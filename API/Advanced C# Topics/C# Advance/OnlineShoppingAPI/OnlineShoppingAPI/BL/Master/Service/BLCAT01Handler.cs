using OnlineShoppingAPI.BL.Master.Interface;
using OnlineShoppingAPI.DL;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.POCO;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;
using System.Web;
using static OnlineShoppingAPI.BL.Common.BLHelper;

namespace OnlineShoppingAPI.BL.Master.Service
{
    /// <summary>
    /// Service class for managing <see cref="CAT01"/> business logic.
    /// </summary>
    public class BLCAT01Handler : ICAT01Service
    {
        #region Private Fields

        /// <summary>
        /// DB Context of <see cref="CAT01"/>.
        /// </summary>
        private readonly DBCAT01Context _dbCAT01Context;

        /// <summary>
        /// Orm Lite Connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// <see cref="CAT01"/> object instance for request.
        /// </summary>
        private CAT01 _objCAT01;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Specifies the operation to perform.
        /// </summary>
        public EnmOperation Operation { get; set; }

        #endregion Public Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BLCAT01Handler"/> class.
        /// </summary>
        public BLCAT01Handler()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            _dbCAT01Context = new DBCAT01Context();
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to be deleted.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response Delete(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<CAT01>(_objCAT01);
            }

            return OkResponse("Category deleted successfully.");
        }

        /// <summary>
        /// Validate the id before the delete process.
        /// </summary>
        /// <param name="id">The ID of the category to be deleted.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response DeleteValidation(int id)
        {
            if (IsCAT01Exist(id))
                return OkResponse();

            return NotFoundResponse("Category not found.");
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GetAll()
        {
            DataTable dtCAT01 = _dbCAT01Context.GetAll();

            if (dtCAT01.Rows.Count == 0)
                return NoContentResponse();

            return OkResponse("", dtCAT01);
        }

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GetById(int id)
        {
            if (IsCAT01Exist(id))
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    _objCAT01 = db.SingleById<CAT01>(id);
                }

                return OkResponse("", _objCAT01);
            }

            return NotFoundResponse("Category not found.");
        }

        /// <summary>
        /// Prepares category object for saving a category.
        /// </summary>
        /// <param name="objDTOCAT01">Data Transfer Object representing the category.</param>
        public void PreSave(DTOCAT01 objDTOCAT01)
        {
            _objCAT01 = objDTOCAT01.ConvertTo<CAT01>();
        }

        /// <summary>
        /// Checking the id exists or not for category.
        /// </summary>
        /// <param name="objDTOCAT01">DTO for CAT01 Model.</param>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response PreValidation(DTOCAT01 objDTOCAT01)
        {
            if (Operation == EnmOperation.E)
            {
                if (!IsCAT01Exist(objDTOCAT01.T01F01))
                {
                    return NotFoundResponse("Category not found.");
                }
            }

            return OkResponse();
        }

        /// <summary>
        /// Create or Updates the category information.
        /// </summary>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response Save()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                if (Operation == EnmOperation.A)
                {
                    db.Insert(_objCAT01);
                    return OkResponse("Category created successfully.");
                }

                db.Update(_objCAT01);
                return OkResponse("Category updated successfully.");
            }
        }

        /// <summary>
        /// Validates category information.
        /// </summary>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response Validation()
        {
            bool isDuplicateT01F02;

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                isDuplicateT01F02 = db.Exists<CAT01>(c => c.T01F02 == _objCAT01.T01F02);
            }

            if (isDuplicateT01F02)
                return PreConditionFailedResponse("Category name already exists choose another name.");

            return OkResponse();
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Checks the category exists or not.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool IsCAT01Exist(int id)
        {
            bool isExist;

            using (var db = _dbFactory.OpenDbConnection())
            {
                isExist = db.Exists<CAT01>(c => c.T01F01 == id);
            }

            return isExist;
        }

        #endregion Private Methods
    }
}