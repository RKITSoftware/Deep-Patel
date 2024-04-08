using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;

namespace OnlineShoppingAPI.BL.Interface
{
    public interface IPRO02Service
    {
        void Delete(int id, out Response response);
        void GetAll(out Response response);
        void GetInformation(out Response response);
        void PreSave(DTOPRO02 objPRO02DTO, EnmOperation operation);
        void Save(out Response response);
        void UpdateSellPrice(int id, int sellPrice, out Response response);
        bool Validation(out Response response);
    }
}