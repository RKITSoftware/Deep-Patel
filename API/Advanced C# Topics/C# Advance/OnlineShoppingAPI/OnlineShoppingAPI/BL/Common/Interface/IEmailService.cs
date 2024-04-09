using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShoppingAPI.BL.Common.Interface
{
    public interface IEmailService
    {
        Task SendAsync(string customerEmail, List<dynamic> lstItems);
        void Send(string email, string otp);
    }
}