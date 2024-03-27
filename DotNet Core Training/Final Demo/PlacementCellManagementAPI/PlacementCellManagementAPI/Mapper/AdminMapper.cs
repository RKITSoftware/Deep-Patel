using PlacementCellManagementAPI.Dtos;
using PlacementCellManagementAPI.Models;

namespace PlacementCellManagementAPI.Mapper
{
    /// <summary>
    /// Mapper class responsible for mapping DtoADM01 objects to ADMUSR objects.
    /// </summary>
    public static class AdminMapper
    {
        /// <summary>
        /// Maps a DtoADM01 object to an ADMUSR object.
        /// </summary>
        /// <param name="objAdminDto">DtoADM01 object to be mapped.</param>
        /// <returns>An ADMUSR object mapped from the provided DtoADM01 object.</returns>
        public static ADMUSR ToPOCO(DtoADM01 objAdminDto)
        {
            // Map properties from DtoADM01 to ADMUSR objects
            return new ADMUSR()
            {
                M01 = new ADM01()
                {
                    M01F02 = objAdminDto.M01101.Split(' ')[0],
                    M01F03 = objAdminDto.M01101.Split(' ')[1],
                    M01F04 = objAdminDto.M01102,
                    M01F05 = objAdminDto.M01103
                },
                R01 = new USR01()
                {
                    R01F02 = objAdminDto.M01104,
                    R01F03 = objAdminDto.M01105,
                    R01F04 = objAdminDto.M01106,
                    R01F05 = "Admin"
                }
            };
        }
    }
}
