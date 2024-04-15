using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.Extension;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
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
    public class BLPRO01Handler : IPRO01Service
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

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Specifies the operation to perform.
        /// </summary>
        public EnmOperation Operation { get; set; }

        #endregion Public Properties

        #region Constructor

        /// <summary>
        /// Initialize instance of <see cref="BLPRO01Handler"/>.
        /// </summary>
        public BLPRO01Handler()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Deletes the customer specified by Id.
        /// </summary>
        /// <param name="id">Customer id.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response Delete(int id)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    PRO01 existingPRO01 = db.SingleById<PRO01>(id);

                    if (existingPRO01 == null)
                        return NotFoundResponse("Product not found.");

                    db.Delete(existingPRO01);
                }
            }
            catch (Exception ex) { throw ex; }

            return OkResponse("Product deleted successfully.");
        }

        /// <summary>
        /// Retrieves all customer information.
        /// </summary>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GetAll()
        {
            List<PRO01> lstPRO01;

            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                    lstPRO01 = db.Select<PRO01>();
            }
            catch (Exception ex) { throw ex; }

            if (lstPRO01 == null || lstPRO01.Count == 0)
                return NoContentResponse();

            return OkResponse("", lstPRO01);
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
        /// Checks the record exists or not for operation.
        /// </summary>
        /// <param name="objDTOPRO01">DTO containing the Product information.</param>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response PreValidation(DTOPRO01 objDTOPRO01)
        {
            if (Operation == EnmOperation.A)
            {
                if (objDTOPRO01.O01F01 != 0)
                    return PreConditionFailedResponse("Id needs to be zero for the add operation.");
            }
            else
            {
                if (objDTOPRO01.O01F01 <= 0)
                    return PreConditionFailedResponse("Id needs to greater than zero for update operation.");
            }

            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Update operation prevalidation
                    if (Operation == EnmOperation.E)
                    {
                        if (db.SingleById<PRO01>(objDTOPRO01.O01F01) == null)
                            return NotFoundResponse("Product doesn't exist.");
                    }

                    // Checks supplier exists or not.
                    if (db.SingleById<SUP01>(objDTOPRO01.O01F06) == null)
                        return NotFoundResponse("Supplier doesn't exist.");
                }
            }
            catch (Exception ex) { throw ex; }

            return OkResponse();
        }

        /// <summary>
        /// Performs the create or update operation.
        /// </summary>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response Save()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (Operation == EnmOperation.A)
                    {
                        db.Insert(_objPRO01);
                        return OkResponse("Product created successfully.");
                    }

                    db.Update(_objPRO01);
                    return OkResponse("Product updated successfully.");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Update the quantity of product.
        /// </summary>
        /// <param name="id">Product Id.</param>
        /// <param name="quantity">Quantity that user wants to add.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response UpdateQuantity(int id, int quantity)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    PRO01 objProduct = db.SingleById<PRO01>(id);

                    if (objProduct == null)
                        return NotFoundResponse("Product not found.");

                    // Update product quantity
                    objProduct.O01F04 += quantity;
                    db.Update(objProduct);
                }
            }
            catch (Exception ex) { throw ex; }

            return OkResponse("Quantity updates successfully.");
        }

        /// <summary>
        /// Validates the objects before saving them.
        /// </summary>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response Validation()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (db.Exists<PRO01>(p => p.O01F02 == _objPRO01.O01F02 && p.O01F06 == _objPRO01.O01F06))
                        return PreConditionFailedResponse("Product can't be created because it already exists.");
                }
            }
            catch (Exception ex) { throw ex; }

            return OkResponse();
        }

        #endregion Public Methods
    }
}