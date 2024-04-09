using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;

namespace OnlineShoppingAPI.BL.Interface
{
    public interface ICRT01Service
    {
        void BuySingleItem(int id, out Response response);
        void Delete(int id, out Response response);
        void Generate(int id, out Response response);
        void GetCUS01CRT01Details(int id, out Response response);
        void GetFullCRT01InfoOfCUS01(int id, out Response response);
        void PreSave(DTOCRT01 objDTOCRT01, EnmOperation operation);
        void Save(out Response response);
        bool Validation(out Response response);
        void VerifyAndBuy(int id, string otp, out Response response);
    }
}