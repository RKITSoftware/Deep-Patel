using SchoolManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace SchoolManagementAPI.Business_Logic
{
    /// <summary>
    /// Business Logic class for handling administrative operations.
    /// </summary>
    public class BLAdmin
    {
        /// <summary>
        /// Creates a new admin along with associated user and assigns IDs.
        /// </summary>
        /// <param name="objADMUSR">Admin and User information to be created.</param>
        /// <returns>HTTP response indicating the success or failure of the operation.</returns>
        public HttpResponseMessage Create(ADMUSR objADMUSR)
        {
            try
            {
                // Check if input is null
                if (objADMUSR == null)
                {
                    return BLHelper.ResponseMessage(HttpStatusCode.PreconditionFailed,
                        "Data is null.");
                }

                // Assign IDs and roles
                objADMUSR.objAdmin.M01F01 = BLHelper.adminID + 1;
                objADMUSR.objAdmin.M01F04 = objADMUSR.objUser.R01F02;

                objADMUSR.objUser.R01F01 = BLHelper.userID + 1;
                objADMUSR.objUser.R01F04 = "Admin";

                // Increment global IDs
                BLHelper.adminID++;
                BLHelper.userID++;

                // Add to respective lists
                BLHelper.lstAdmin.Add(objADMUSR.objAdmin);
                BLHelper.lstUsers.Add(objADMUSR.objUser);

                return BLHelper.ResponseMessage(HttpStatusCode.Created,
                    "Admin Created Successfully");
            }
            catch (Exception ex)
            {
                // Log and handle exceptions
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred during creating admin.");
            }
        }

        /// <summary>
        /// Retrieves a list of all admins.
        /// </summary>
        /// <returns>List of admins or null if an error occurs.</returns>
        public List<ADM01> GetAll()
        {
            try
            {
                // Return the list of admins
                return BLHelper.lstAdmin;
            }
            catch (Exception ex)
            {
                // Log and handle exceptions
                BLHelper.LogError(ex);
                return null;
            }
        }

        /// <summary>
        /// Deletes an admin based on the provided ID.
        /// </summary>
        /// <param name="id">ID of the admin to be deleted.</param>
        /// <returns>HTTP response indicating the success or failure of the operation.</returns>
        internal HttpResponseMessage Delete(int id)
        {
            try
            {
                // Check if ID is valid
                if (id <= 0)
                {
                    return BLHelper.ResponseMessage(HttpStatusCode.PreconditionFailed,
                        "Id can't be negative nor zero.");
                }

                // Find existing admin
                ADM01 existingAdmin = BLHelper.lstAdmin.FirstOrDefault(adm => adm.M01F01 == id);

                if (existingAdmin == null)
                {
                    return BLHelper.ResponseMessage(HttpStatusCode.NotFound,
                        "Admin can't be found.");
                }

                // Remove admin and associated user
                BLHelper.lstAdmin.Remove(existingAdmin);
                BLHelper.lstUsers.RemoveAll(usr => usr.R01F02.Equals(existingAdmin.M01F04));

                return BLHelper.ResponseMessage(HttpStatusCode.OK,
                    "Admin deleted successfully.");
            }
            catch (Exception ex)
            {
                // Log and handle exceptions
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred during deleting admin.");
            }
        }

        /// <summary>
        /// Retrieves an admin based on the provided ID.
        /// </summary>
        /// <param name="id">ID of the admin to be retrieved.</param>
        /// <returns>Admin object or null if not found.</returns>
        internal ADM01 Get(int id)
        {
            try
            {
                // Find and return admin by ID
                return BLHelper.lstAdmin.FirstOrDefault(adm => adm.M01F01 == id);
            }
            catch (Exception ex)
            {
                // Log and handle exceptions
                BLHelper.LogError(ex);
                return null;
            }
        }

        /// <summary>
        /// Updates an existing admin's information.
        /// </summary>
        /// <param name="objAdmin">Updated admin information.</param>
        /// <returns>HTTP response indicating the success or failure of the operation.</returns>
        internal HttpResponseMessage Update(ADM01 objAdmin)
        {
            try
            {
                // Check if input is null
                if (objAdmin == null)
                {
                    return BLHelper.ResponseMessage(HttpStatusCode.PreconditionFailed,
                        "Id can't be null nor negative");
                }

                // Find existing admin
                ADM01 existingAdmin = BLHelper.lstAdmin.FirstOrDefault(adm =>
                    adm.M01F01 == objAdmin.M01F01 && adm.M01F04.Equals(objAdmin.M01F04));

                if (existingAdmin == null)
                    return BLHelper.ResponseMessage(HttpStatusCode.NotFound,
                        "Admin doesn't exist");

                // Update admin details
                existingAdmin.M01F02 = objAdmin.M01F02;
                existingAdmin.M01F03 = objAdmin.M01F03;

                return BLHelper.ResponseMessage(HttpStatusCode.OK,
                    "Admin updated successfully.");
            }
            catch (Exception ex)
            {
                // Log and handle exceptions
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred during updating admin.");
            }
        }
    }
}
