using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;

namespace OnlineShoppingAPI.BL.Interface
{
    public interface IPRO01Service
    {
        void Delete(int id, out Response response);
        void GetAll(out Response response);
        void PreSave(DTOPRO01 objPRO01DTO, EnmOperation operation);
        void Save(out Response response);
        void UpdateQuantity(int id, int quantity, out Response response);
        bool Validation(out Response response);
    }
}