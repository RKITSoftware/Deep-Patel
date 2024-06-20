using DataGridFinalDemoAPI.Models;

namespace DataGridFinalDemoAPI.Business_Logic.Interfaces
{
    public interface ICTY01Service
    {
        List<CTY01> GetAll();

        CTY01 Get(int id);
    }
}
