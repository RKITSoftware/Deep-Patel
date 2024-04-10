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
using System.Web;
using static OnlineShoppingAPI.BL.Common.BLHelper;

namespace OnlineShoppingAPI.BL.Service
{
    /// <summary>
    /// Service class for managing <see cref="CAT01"/> business logic.
    /// </summary>
    public class BLCAT01 : ICAT01Service
    {
        #region Private Fields

        /// <summary>
        /// Orm Lite Connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// <see cref="CAT01"/> object instance for request.
        /// </summary>
        private CAT01 _objCAT01;

        /// <summary>
        /// DB Context of <see cref="CAT01"/>.
        /// </summary>
        private readonly DBCAT01 _context;

        #endregion

        #region Public Properties

        /// <summary>
        /// Specifies the operation to perform.
        /// </summary>
        public EnmOperation Operation { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BLCAT01"/> class.
        /// </summary>
        public BLCAT01()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            _context = new DBCAT01();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Checking the id exists or not for category.
        /// </summary>
        /// <param name="objDTOCAT01">DTO for CAT01 Model.</param>
        /// <param name="response">Response contaiining the outceom of the operation.</param>
        /// <returns></returns>
        public bool PreValidation(DTOCAT01 objDTOCAT01, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Update Operation
                    if (Operation == EnmOperation.Update)
                    {
                        // Checks the category exists or not.
                        if (!db.Exists<CAT01>(c => c.T01F01 == objDTOCAT01.T01101))
                        {
                            response = NotFoundResponse("Category not found.");
                            return false;
                        }
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
        /// Prepares category object for saving a category.
        /// </summary>
        /// <param name="objDTOCAT01">Data Transfer Object representing the category.</param>
        public void PreSave(DTOCAT01 objDTOCAT01)
            => _objCAT01 = objDTOCAT01.Convert<CAT01>();

        /// <summary>
        /// Validates category information.
        /// </summary>
        /// <param name="response">Out parameter containing the validation result.</param>
        /// <returns>True if the category information is valid, otherwise false.</returns>
        public bool Validation(out Response response)
        {
            // Validation

            response = null;
            return true;
        }

        /// <summary>
        /// Create or Updates the category information.
        /// </summary>
        /// <param name="operation">Operation type for the save action.</param>
        /// <param name="response">Out parameter containing the response status after saving.</param>
        public void Save(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (Operation == EnmOperation.Create)
                    {
                        db.Insert(_objCAT01);
                        response = OkResponse("Category created successfully.");
                    }
                    else
                    {
                        db.Update(_objCAT01);
                        response = OkResponse("Category updated successfully.");
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
        /// Retrieves all categories.
        /// </summary>
        /// <param name="response">Out parameter containing the response with all categories.</param>
        public void GetAll(out Response response) => _context.GetAll(out response);

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <param name="response">Out parameter containing the response with the requested category.</param>
        public void GetById(int id, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    CAT01 objCategory = db.Single<CAT01>(c => c.T01F01 == id);

                    if (objCategory == null)
                    {
                        response = NotFoundResponse("Category not found.");
                    }
                    else
                    {
                        response = OkResponse("Category found successfully.");
                        response.Data = objCategory;
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
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to be deleted.</param>
        /// <param name="response">Out parameter containing the response status after deletion.</param>
        public void Delete(int id, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    CAT01 category = db.SingleById<CAT01>(id);

                    if (category == null)
                    {
                        response = NotFoundResponse("Category not found.");
                        return;
                    }

                    db.Delete(category);
                    response = OkResponse("Category deleted successfully.");
                }
            }
            catch (Exception exception)
            {
                exception.LogException();
                response = ISEResponse();
            }
        }

        #endregion
    }
}