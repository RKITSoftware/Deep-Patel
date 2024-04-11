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
using System.Collections.Generic;
using System.Net;
using System.Web;
using static OnlineShoppingAPI.BL.Common.BLHelper;

namespace OnlineShoppingAPI.BL.Service
{
    /// <summary>
    /// Service implementation of <see cref="IPRO02Service"/>.
    /// </summary>
    public class BLPRO02 : IPRO02Service
    {
        #region Private Fields

        /// <summary>
        /// Orm Lite Connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Instance of <see cref="PRO02"/> for create or update related operations.
        /// </summary>
        private PRO02 _objPRO02;

        /// <summary>
        /// Databse Context of <see cref="DBPRO02"/> for MySQL Queries.
        /// </summary>
        private readonly DBPRO02 _dbPRO02;

        #endregion

        #region Public Properties

        /// <summary>
        /// Specifies the operation to perform.
        /// </summary>
        public EnmOperation Operation { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize the instance when it's created.
        /// </summary>
        public BLPRO02()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            _dbPRO02 = new DBPRO02();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Checks the record exists or not for operation.
        /// </summary>
        /// <param name="objDTOPRO02">DTO containing the Product information.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        /// <returns>True if pre validation successful else false.</returns>
        public bool PreValidation(DTOPRO02 objDTOPRO02, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Checks category exists or not.
                    if (db.SingleById<CAT01>(objDTOPRO02.O02109) == null)
                    {
                        response = NotFoundResponse("Category doesn't exist.");
                        return false;
                    }

                    // Checks supplier exists or not.
                    if (db.SingleById<SUP01>(objDTOPRO02.O02110) == null)
                    {
                        response = NotFoundResponse("Supplier doesn't exist.");
                        return false;
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
        /// Validates the objects before the save process.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        /// <returns>True if validation Successful, else false.</returns>
        public bool Validation(out Response response)
        {
            response = null;
            return true;
        }

        /// <summary>
        /// Save the object information to the database according to opertaion.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void Save(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Create
                    db.Insert(_objPRO02);
                    response = OkResponse("Product created successfully.");
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
        }

        /// <summary>
        /// Deletes the product specified by id.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void Delete(int id, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (!db.Exists<PRO02>(p => p.O02F01 == id))
                    {
                        response = NotFoundResponse("Product not found.");
                        return;
                    }

                    db.DeleteById<PRO02>(id);
                    response = OkResponse("Product created successfully.");
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
        }

        /// <summary>
        /// Retrieves all products information from the database.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void GetAll(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    List<PRO02> lstPRO02 = db.Select<PRO02>();

                    if (lstPRO02 == null || lstPRO02.Count == 0)
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.NoContent,
                            Message = "Product data doesn't available."
                        };
                    }
                    else
                    {
                        response = OkResponse("Success.");
                        response.Data = lstPRO02;
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
        /// Updates the sell price of the specified product which id is given.
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="sellPrice">Updates sell price.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void UpdateSellPrice(int id, int sellPrice, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    PRO02 existingProduct = db.SingleById<PRO02>(id);

                    if (existingProduct == null)
                    {
                        response = NotFoundResponse("Product not found.");
                    }
                    else
                    {
                        existingProduct.O02F04 = sellPrice;
                        db.Update(existingProduct);

                        response = OkResponse("Product sell price updated successfully.");
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
        /// Gets the product's full information using DB.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void GetInformation(out Response response) => _dbPRO02.GetInformation(out response);

        #endregion
    }
}