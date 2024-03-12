using DtoDemo.Dto;
using DtoDemo.Model;
using System;

namespace DtoDemo.Mapper
{
    public static class EmployeeMapper
    {
        public static EMP01 DtoToPoco(DtoEMP01 objEmployeeDto)
        {
            return new EMP01()
            {
                P01F01 = 0,
                P01F02 = objEmployeeDto.P01101.Split(' ')[0],
                P01F03 = objEmployeeDto.P01101.Split(' ')[1],
                P01F04 = objEmployeeDto.P01102,
                P01F05 = DateTime.Now
            };
        }

        public static DtoEMP01 PocoToDto(EMP01 objEmployee)
        {
            return new DtoEMP01()
            {
                P01101 = $"{objEmployee.P01F02} {objEmployee.P01F03}",
                P01102 = objEmployee.P01F04
            };
        }
    }
}
