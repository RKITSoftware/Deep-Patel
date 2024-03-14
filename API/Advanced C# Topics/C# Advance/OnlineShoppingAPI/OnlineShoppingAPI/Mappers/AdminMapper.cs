using OnlineShoppingAPI.Business_Logic;
using OnlineShoppingAPI.Dto;
using OnlineShoppingAPI.Models;

namespace OnlineShoppingAPI.Mappers
{
    public static class AdminMapper
    {
        public static ADMUSR ToPoco(DtoADM01 objAdminDto)
        {
            return new ADMUSR()
            {
                M01 = new ADM01()
                {
                    M01F02 = objAdminDto.M01101,
                    M01F03 = objAdminDto.M01102,
                },
                R01 = new USR01()
                {
                    R01F02 = objAdminDto.M01102.Split('@')[0],
                    R01F03 = objAdminDto.M01103,
                    R01F04 = "Admin",
                    R01F05 = BLHelper.GetEncryptPassword(objAdminDto.M01103)
                }
            };
        }
    }
}