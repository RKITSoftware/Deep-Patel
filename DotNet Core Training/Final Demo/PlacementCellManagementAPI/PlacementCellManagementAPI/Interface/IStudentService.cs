using PlacementCellManagementAPI.Dtos;

namespace PlacementCellManagementAPI.Interface
{
    /// <summary>
    /// Interface for student service operations.
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// Adds a new student.
        /// </summary>
        /// <param name="objStudentDto">The DTO representing the student to add.</param>
        /// <returns>True if the student was added successfully, otherwise false.</returns>
        bool Add(DtoSTU01 objStudentDto);
    }
}
