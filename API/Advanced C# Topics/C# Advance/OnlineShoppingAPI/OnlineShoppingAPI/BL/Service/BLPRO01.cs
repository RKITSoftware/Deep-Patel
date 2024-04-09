using OnlineShoppingAPI.BL.Interface;
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
using static OnlineShoppingAPI.BL.Common.BLHelper;

namespace OnlineShoppingAPI.BL.Service
{
    /// <summary>
    /// Service implementation of the <see cref="IPRO01Service"/> interface.
    /// </summary>
    public class BLPRO01 : IPRO01Service
    {
        #region Private Fields

        /// <summary>
        /// Orm Lite connextion.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Object of <see cref="PRO01"/> for creating or updating related operatios.
        /// </summary>
        private PRO01 _objPRO01;

        /// <summary>
        /// Enum to specify which operation is performing.
        /// </summary>
        private EnmOperation _operation;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize instance of <see cref="BLPRO01"/>.
        /// </summary>
        public BLPRO01()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialize the object of <see cref="PRO01"/> and prepare it for create or delete operation.
        /// </summary>
        /// <param name="objPRO01DTO">DTO of product.</param>
        /// <param name="operation">Operation to perform.</param>
        public void PreSave(DTOPRO01 objPRO01DTO, EnmOperation operation)
        {
            _objPRO01 = objPRO01DTO.Convert<PRO01>();
            _operation = operation;
        }

        /// <summary>
        /// Validates the objects before saving them.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        /// <returns><see langword="true"/> if validation successful, else <see langword="false"/>.</returns>
        public bool Validation(out Response response)
        {
            response = null;
            return true;
        }

        /// <summary>
        /// Performs the create or update operation.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void Save(out Response response)
        {
            if (_operation == EnmOperation.Create)
                Create(out response);
            else
                Update(out response);
        }

        /// <summary>
        /// Deletes the customer specified by Id.
        /// </summary>
        /// <param name="id">Customer id.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void Delete(int id, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    PRO01 product = db.SingleById<PRO01>(id);
                    if (product == null)
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Product Not Found."
                        };
                    }
                    else
                    {
                        db.DeleteById<PRO01>(id);
                        response = OkResponse();
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
        /// Retrieves all customer information.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void GetAll(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    List<PRO01> lstPRO01 = db.Select<PRO01>();

                    response = OkResponse();
                    response.Data = lstPRO01;
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
        }

        /// <summary>
        /// Update the quantity of product.
        /// </summary>
        /// <param name="id">Product Id.</param>
        /// <param name="quantity">Quantity that user wants to add.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void UpdateQuantity(int id, int quantity, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    PRO01 objProduct = db.SingleById<PRO01>(id);

                    if (objProduct == null)
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.BadRequest,
                            Message = "Id doesn't reference to product."
                        };
                        return;
                    }

                    // Update product quantity
                    objProduct.O01F04 += quantity;
                    db.Update(objProduct);

                    response = OkResponse();
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
        /// Updates the existing customer information.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        private void Update(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    PRO01 existingProduct = db.SingleById<PRO01>(_objPRO01.O01F01);

                    if (existingProduct == null)
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.BadRequest,
                            Message = "Product id doesn't exist."
                        };
                        return;
                    }

                    // Update product properties
                    existingProduct.O01F02 = _objPRO01.O01F02;
                    existingProduct.O01F03 = _objPRO01.O01F03;
                    existingProduct.O01F04 = _objPRO01.O01F04;
                    existingProduct.O01F05 = _objPRO01.O01F05;

                    // Perform the database update
                    db.Update(existingProduct);

                    response = OkResponse();
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        private void Create(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.Insert(_objPRO01);

                    response = new Response()
                    {
                        StatusCode = HttpStatusCode.Created,
                        Message = "Product Successfully created."
                    };
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
        }

        #endregion
    }
}