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

        #endregion

        #region Public Properties

        /// <summary>
        /// Specifies the operation to perform.
        /// </summary>
        public EnmOperation Operation { get; set; }

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
        /// Checks the record exists or not for operation.
        /// </summary>
        /// <param name="objDTOPRO01">DTO containing the Product information.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        /// <returns>True if pre validation successful else false.</returns>
        public bool PreValidation(DTOPRO01 objDTOPRO01, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Checks supplier exists or not.
                    if (db.SingleById<SUP01>(objDTOPRO01.O01106) == null)
                    {
                        response = NotFoundResponse("Supplier doesn't exist.");
                        return false;
                    }

                    // Update operation prevalidation
                    if (Operation == EnmOperation.Update)
                    {
                        if (db.SingleById<PRO01>(objDTOPRO01.O01101) == null)
                        {
                            response = NotFoundResponse("Product doesn't exist.");
                            return false;
                        }
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
        /// Initialize the object of <see cref="PRO01"/> and prepare it for create or delete operation.
        /// </summary>
        /// <param name="objPRO01DTO">DTO of product.</param>
        public void PreSave(DTOPRO01 objPRO01DTO)
        {
            _objPRO01 = objPRO01DTO.Convert<PRO01>();
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
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (Operation == EnmOperation.Create)
                    {
                        db.Insert(_objPRO01);
                        response = OkResponse("Product created successfully.");
                    }
                    else
                    {
                        db.Update(_objPRO01);
                        response = OkResponse("Product updated successfully.");
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
                        response = NotFoundResponse("Product not found.");
                        return;
                    }

                    db.DeleteById<PRO01>(id);
                    response = OkResponse("Product deleted successfully.");
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

                    response = OkResponse("Success");
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
                        response = NotFoundResponse("Product not found.");
                        return;
                    }

                    // Update product quantity
                    objProduct.O01F04 += quantity;
                    db.Update(objProduct);

                    response = OkResponse("Quantity updates successfully.");
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