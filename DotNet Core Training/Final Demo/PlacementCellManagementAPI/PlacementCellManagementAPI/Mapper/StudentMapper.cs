using PlacementCellManagementAPI.Dtos;
using PlacementCellManagementAPI.Models;

namespace PlacementCellManagementAPI.Mapper
{
    /// <summary>
    /// Mapper class for mapping between DtoSTU01 and STUUSR objects.
    /// </summary>
    public static class StudentMapper
    {
        /// <summary>
        /// Maps DtoSTU01 object to STUUSR object.
        /// </summary>
        /// <param name="objStudentDto">DtoSTU01 object to map from.</param>
        /// <returns>STUUSR object mapped from DtoSTU01.</returns>
        public static STUUSR ToPoco(DtoSTU01 objStudentDto)
        {
            // Mapping logic
            return new STUUSR()
            {
                U01 = new STU01()
                {
                    U01F02 = objStudentDto.U01101.Split(' ')[0],
                    U01F03 = objStudentDto.U01101.Split(' ')[1],
                    U01F04 = objStudentDto.U01102,
                    U01F05 = objStudentDto.U01103,
                    U01F06 = objStudentDto.U01104,
                },
                R01 = new USR01()
                {
                    R01F02 = objStudentDto.U01105,
                    R01F03 = objStudentDto.U01106,
                    R01F04 = objStudentDto.U01107,
                    R01F05 = "Student"
                }
            };
        }
    }
}
