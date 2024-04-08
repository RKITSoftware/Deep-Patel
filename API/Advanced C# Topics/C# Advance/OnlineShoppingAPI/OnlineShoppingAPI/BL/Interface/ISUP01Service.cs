using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;

namespace OnlineShoppingAPI.BL.Interface
{
    public interface ISUP01Service
    {
        void ChangeEmail(string username, string password, string newEmail, out Response response);
        void ChangePassword(string username, string oldPassword, string newPassword, out Response response);
        void Delete(int id, out Response response);
        void GetAll(out Response response);
        void GetById(int id, out Response response);
        void PreSave(DTOSUP01 objSUP01DTO, EnmOperation operation);
        void Save(out Response response);
        bool Validation(out Response response);
    }
}