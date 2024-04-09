using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;
using OnlineShoppingAPI.Models.POCO;
using System.Collections.Generic;
using System.Net.Http;

namespace OnlineShoppingAPI.BL.Interface
{
    public interface IRCD01Service
    {
        bool BuyCartItems(List<CRT01> lstItems, string s01F03, out Response response);
        void Delete(int id, out Response response);
        HttpResponseMessage Download(int id, string filetype);
        void GetAllRecord(out Response response);
        void PreSave(DTORCD01 objDTORCD01, EnmOperation operation);
        void Save(out Response response);
        bool Validation(out Response response);
    }
}