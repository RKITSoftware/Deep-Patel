using PlacementCellManagementAPI.Dtos;
using System.Data;

namespace PlacementCellManagementAPI.Interface
{
    /// <summary>
    /// Interface defining operations for job services.
    /// </summary>
    public interface IJobService
    {
        /// <summary>
        /// Adds a new job.
        /// </summary>
        /// <returns>True if addition succeeds, false otherwise.</returns>
        bool Add();

        /// <summary>
        /// Deletes a job by its ID.
        /// </summary>
        /// <param name="id">The ID of the job to delete.</param>
        /// <returns>True if deletion succeeds, false otherwise.</returns>
        bool Delete(int id);

        /// <summary>
        /// Retrieves all jobs.
        /// </summary>
        /// <returns>The DataTable containing all jobs.</returns>
        DataTable GetAll();

        /// <summary>
        /// Prepares the job object for saving.
        /// </summary>
        /// <param name="objJobDto">The DTO containing job information.</param>
        void PreSave(DtoJOB01 objJobDto);

        /// <summary>
        /// Validates the job data.
        /// </summary>
        /// <returns>True if data is valid, false otherwise.</returns>
        bool Validation();
    }
}
