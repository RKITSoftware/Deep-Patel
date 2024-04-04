using PlacementCellManagementAPI.Models.Dtos;

namespace PlacementCellManagementAPI.Business_Logic.Interface
{
    /// <summary>
    /// Interface for student service operations.
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// Adds a new student to the system.
        /// </summary>
        /// <returns>True if the student was added successfully, otherwise false.</returns>
        bool Add();

        /// <summary>
        /// Deletes a student from the system by ID.
        /// </summary>
        /// <param name="id">The ID of the student to delete.</param>
        /// <returns>True if the student was deleted successfully, otherwise false.</returns>
        bool Delete(int id);

        /// <summary>
        /// Performs pre-save operations on the student DTO object.
        /// </summary>
        /// <param name="objStudentDto">The DTO object representing the student.</param>
        void PreSave(DtoSTU01 objStudentDto);

        /// <summary>
        /// Performs validation checks on the student data.
        /// </summary>
        /// <returns>True if the student data is valid, otherwise false.</returns>
        bool Validation();
    }
}
