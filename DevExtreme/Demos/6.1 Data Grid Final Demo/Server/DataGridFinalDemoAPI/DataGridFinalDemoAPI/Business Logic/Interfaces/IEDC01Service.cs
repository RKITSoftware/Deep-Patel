using DataGridFinalDemoAPI.Models;

namespace DataGridFinalDemoAPI.Business_Logic.Interfaces
{
    public interface IEDC01Service
    {
        List<EDC01> GetAll(int id);
    }
}
