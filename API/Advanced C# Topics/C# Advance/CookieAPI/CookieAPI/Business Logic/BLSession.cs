using System;
using System.Web;
using System.Web.SessionState;

namespace CookieAPI.Business_Logic
{
    public class BLSession : SessionIDManager
    {
        public override string CreateSessionID(HttpContext context)
        {
            string sessionId = Guid.NewGuid().ToString();
            return sessionId;
        }

        public override bool Validate(string id)
        {
            bool isValidId = id == HttpContext.Current.Session.SessionID;
            return isValidId;
        }
    }
}