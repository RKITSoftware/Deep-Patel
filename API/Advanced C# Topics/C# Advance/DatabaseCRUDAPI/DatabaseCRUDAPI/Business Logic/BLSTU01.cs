using DatabaseCRUDAPI.DAL;
using DatabaseCRUDAPI.Models.DTO;
using DatabaseCRUDAPI.Models.Enums;
using DatabaseCRUDAPI.Models.POCO;
using System;
using System.Collections.Generic;

namespace DatabaseCRUDAPI.Business_Logic
{
    /// <summary>
    /// Business logic layer for managing CRUD operations related to student data.
    /// </summary>
    public class BLSTU01
    {
        #region Private Fields

        /// <summary>
        /// STU01 poco model object for http api request.
        /// </summary>
        private STU01 _objSTU01;

        /// <summary>
        /// Mysql Queries execute file.
        /// </summary>
        private readonly DBSTU01 _context;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BLSTU01 class.
        /// </summary>
        public BLSTU01()
        {
            _context = new DBSTU01();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Prepares the student object for saving based on the operation type.
        /// </summary>
        /// <param name="objSTU01DTO">Student DTO containing data to be saved.</param>
        /// <param name="operation">The type of operation (Create or Update).</param>
        public void PreSave(DTOSTU01 objSTU01DTO, EnmOperation operation)
        {
            DateTime currentTime = DateTime.Now;

            _objSTU01 = new STU01()
            {
                U01F02 = objSTU01DTO.U01102,
                U01F03 = objSTU01DTO.U01103,
                U01F05 = currentTime
            };

            if (operation == EnmOperation.Create)
            {
                _objSTU01.U01F04 = currentTime;
            }

            if (operation == EnmOperation.Update)
            {
                _objSTU01.U01F01 = objSTU01DTO.U01101;
            }
        }

        /// <summary>
        /// Performs validation on the student object.
        /// </summary>
        /// <returns>True if the object passes validation, otherwise false.</returns>
        public bool Validation()
        {
            if (string.IsNullOrEmpty(_objSTU01.U01F02))
            {
                return false;
            }

            if (_objSTU01.U01F03 <= 0 || _objSTU01.U01F03 >= 99)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Saves the student object based on the operation type.
        /// </summary>
        /// <param name="operation">The type of operation (Create or Update).</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public bool Save(EnmOperation operation)
        {
            if (operation == EnmOperation.Create)
            {
                return _context.Create(_objSTU01);
            }

            return _context.Update(_objSTU01);
        }

        /// <summary>
        /// Retrieves all student data.
        /// </summary>
        /// <returns>A list of student objects.</returns>
        public List<STU01> ReadData() => _context.GetAllSTU01Data();

        /// <summary>
        /// Deletes a student record by ID.
        /// </summary>
        /// <param name="id">The ID of the student record to be deleted.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public bool Delete(int id) => _context.Delete(id);

        #endregion
    }
}
