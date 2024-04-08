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
using System.Net;
using System.Web;

namespace OnlineShoppingAPI.BL.Service
{
    /// <summary>
    /// Service class for managing CAT01 business logic.
    /// </summary>
    public class BLCAT01 : ICAT01Service
    {
        /// <summary>
        /// Orm Lite Connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Category object instance for request.
        /// </summary>
        private CAT01 _objCAT01;

        /// <summary>
        /// DB Context for MySql Queries.
        /// </summary>
        private readonly DBCAT01 _context;

        /// <summary>
        /// Initializes a new instance of the BLCAT01 class.
        /// </summary>
        public BLCAT01()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            _context = new DBCAT01();
        }

        /// <summary>
        /// Prepares for saving a category.
        /// </summary>
        /// <param name="objDTOCAT01">Data Transfer Object representing the category.</param>
        /// <param name="operation">Operation type for the save action.</param>
        public void PreSave(DTOCAT01 objDTOCAT01, EnmOperation operation)
        {
            _objCAT01 = objDTOCAT01.Convert<CAT01>();
        }

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
        /// Saves changes made to a category.
        /// </summary>
        /// <param name="operation">Operation type for the save action.</param>
        /// <param name="response">Out parameter containing the response status after saving.</param>
        public void Save(EnmOperation operation, out Response response)
        {
            if (operation == EnmOperation.Create)
                Create(out response);
            else
                Update(out response);
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="response">Out parameter containing the response status after creation.</param>
        private void Create(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.Insert(_objCAT01);

                    response = new Response()
                    {
                        StatusCode = HttpStatusCode.Created,
                        Message = "Category succesfully created."
                    };
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = BLHelper.ISEResponse();
            }
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="response">Out parameter containing the response status after update.</param>
        private void Update(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    bool isExist = db.SingleById<CAT01>(_objCAT01.T01F01) != null;

                    if (!isExist)
                    {
                        response = new Response()
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Category not found."
                        };
                        return;
                    }

                    db.Update(_objCAT01);
                    response = new Response()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Category updated successfully."
                    };
                }
            }
            catch (Exception exception)
            {
                exception.LogException();
                response = BLHelper.ISEResponse();
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
                        response = new Response()
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Category not found."
                        };
                    }
                    else
                    {
                        response = new Response()
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Success.",
                            Data = objCategory
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = BLHelper.ISEResponse();
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
                    // Retrieve category information by id
                    CAT01 category = db.SingleById<CAT01>(id);

                    // Check if the category exists
                    if (category == null)
                    {
                        response = new Response()
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Category doesn't exist."
                        };
                        return;
                    }

                    db.Delete(category);

                    // Return success response
                    response = new Response()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Category Successfully Deleted."
                    };
                }
            }
            catch (Exception exception)
            {
                exception.LogException();
                response = BLHelper.ISEResponse();
            }
        }
    }
}