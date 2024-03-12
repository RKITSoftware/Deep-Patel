using DtoDemo.Dto;
using DtoDemo.Mapper;
using DtoDemo.Model;
using System;
using System.Collections.Generic;

namespace DtoDemo
{
    public class Program
    {
        public static List<EMP01> lstEmployee = new List<EMP01>();

        static void Main(string[] args)
        {
            string fullName = Console.ReadLine();
            int age = Convert.ToInt32(Console.ReadLine());

            DtoEMP01 dtoEMP01 = new DtoEMP01()
            {
                P01101 = fullName,
                P01102 = age
            };

            lstEmployee.Add(EmployeeMapper.DtoToPoco(dtoEMP01));

            foreach (var employee in lstEmployee)
            {
                Console.WriteLine($"{employee.P01F02} - {employee.P01F03}");
            }
        }
    }
}
