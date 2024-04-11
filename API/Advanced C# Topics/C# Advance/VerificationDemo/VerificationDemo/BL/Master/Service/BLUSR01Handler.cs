using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Web;
using VerificationDemo.BL.Master.Interface;
using VerificationDemo.DL;
using VerificationDemo.Extension;
using VerificationDemo.Models;
using VerificationDemo.Models.DTO;
using VerificationDemo.Models.Enum;
using VerificationDemo.Models.POCO;
using static VerificationDemo.BL.Common.Service.BLHelper;

namespace VerificationDemo.BL.Master.Service
{
    /// <summary>
    /// Business logic implementation for the <see cref="USR01"/>.
    /// </summary>
    public class BLUSR01Handler : IUSR01Service
    {
        #region Private Fields

        /// <summary>
        /// Instance of <see cref="USR01"/> model foe create and update operation.
        /// </summary>
        private USR01 _objUSR01;

        /// <summary>
        /// Connection string for the development environment.
        /// </summary>
        private readonly string _devConnectionString;

        /// <summary>
        /// Orm Lite connection interface.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// DB Connection using MySqlConnection
        /// </summary>
        private readonly DBUSR01 _dbUSR01;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or Sets the operation to perform during api request.
        /// </summary>
        public EnmOperation Operation { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor to initialize the <see cref="BLUSR01Handler"/>.
        /// </summary>
        public BLUSR01Handler()
        {
            _devConnectionString = HttpContext.Current.Application["DevConnectionString"] as string;
            _dbFactory = new OrmLiteConnectionFactory(_devConnectionString, MySqlDialect.Provider);

            _dbUSR01 = new DBUSR01(_devConnectionString);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts the dto to poco model conversion and other sets the date-time related fields.
        /// </summary>
        /// <param name="objDto">DTO of the USR01.</param>
        public void PreSave(DTOUSR01 objDto)
        {
            _objUSR01 = objDto.Convert<USR01>();

            if (Operation == EnmOperation.Create)
            {
                _objUSR01.R01F04 = DateTime.Now;
            }
            else
            {
                _objUSR01.R01F05 = DateTime.Now;
            }
        }

        /// <summary>
        /// Performs the cheking operation of Primary key and Foreign Keys of the given DTO.
        /// </summary>
        /// <param name="objDto">DTO of the USR01.</param>
        /// <returns>Response indicating the outcome of the operation.</returns>
        public Response PreValidation(DTOUSR01 objDTOUSR01)
        {
            if (Operation == EnmOperation.Create)
            {
                if (objDTOUSR01.R01F01 != 0)
                {
                    return PreConditionFailedResponse("Id needs to be zero for the add operation");
                }
            }
            else
            {
                if (objDTOUSR01.R01F01 == 0)
                {
                    return PreConditionFailedResponse("Id can't be zero.");
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (db.SingleById<USR01>(objDTOUSR01.R01F01) == null)
                    {
                        return NotFoundResponse("User doesn't exists");
                    }
                }
            }

            return OkResponse();
        }

        /// <summary>
        /// Saves the models according to specified operation to the database.
        /// </summary>
        /// <returns>Response indicating the outcome of the operation.</returns>
        public Response Save()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (Operation == EnmOperation.Create)
                    {
                        db.CreateTableIfNotExists<USR01>();
                        db.Insert(_objUSR01);
                        return OkResponse("User created succesfully.");
                    }
                    else
                    {
                        db.Update(_objUSR01);
                        return OkResponse("User data updated successfully.");
                    }
                }
            }
            catch (Exception)
            {
                return InternalServerErrorResponse();
            }
        }

        /// <summary>
        /// Performs the validation on the USR01 model.
        /// </summary>
        /// <returns>Response indicating the outcome of the operation.</returns>
        public Response Validation()
        {
            return OkResponse();
        }

        /// <summary>
        /// Get all user data from the database using MySqlConnection.
        /// </summary>
        /// <returns>Response with the data.</returns>
        public Response GetAll()
        {
            DataTable dtUSR01 = _dbUSR01.GetAllData();

            if (dtUSR01.Rows.Count == 0)
            {
                return NotFoundResponse("No data available.");
            }

            Response response = OkResponse();
            response.Data = dtUSR01;

            return response;
        }

        /// <summary>
        /// Delete the user record.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>Response indicating the outcome of the operation.</returns>
        public Response Delete(int id)
        {
            if (id <= 0)
            {
                return PreConditionFailedResponse("Id can't be negative nor zero.");
            }

            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (db.SingleById<USR01>(id) == null)
                    {
                        return NotFoundResponse("User doesn't exist.");
                    }

                    db.DeleteById<USR01>(id);
                }
            }
            catch (Exception)
            {
                return InternalServerErrorResponse();
            }

            return OkResponse();
        }

        #endregion
    }
}