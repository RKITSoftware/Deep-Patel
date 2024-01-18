using GenericsAPIController.Interface;
using GenericsAPIController.Models;
using System.Collections.Generic;

namespace GenericsAPIController.Business_Logic
{
    public class BLEmployee : IGenericService<EMP01>
    {
        List<EMP01> lstEmployee = new List<EMP01>();

        public BLEmployee()
        {
            for (int i = 1; i <= 5; i++)
            {
                lstEmployee.Add(new EMP01
                {
                    P01F01 = i,
                    P01F02 = "EMP" + i,
                    P01F03 = 20 + i
                });
            }
        }

        public void Delete(int id)
        {
            lstEmployee.RemoveAll(x => x.P01F01 == id);
        }

        public List<EMP01> GetAll()
        {
            return lstEmployee;
        }

        public EMP01 GetById(int id)
        {
            return lstEmployee.Find(x => x.P01F01 == id);
        }

        public void Insert(EMP01 entity)
        {
            lstEmployee.Add(entity);
        }
    }
}