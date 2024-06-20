using DataGridFinalDemoAPI.Models;

namespace DataGridFinalDemoAPI.Business_Logic.Interfaces
{
    public interface ISTU01Service
    {
        List<STU01> GetAll();
        bool Add(STU01 objSTU01);
        bool Update(STU01 objSTU01);
        bool Delete(int id);

        bool ValidateMobileNumber(int id, string number);
    }
}
