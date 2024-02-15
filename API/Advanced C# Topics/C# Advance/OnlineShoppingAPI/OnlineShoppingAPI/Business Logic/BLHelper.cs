using System.Web.Caching;

namespace OnlineShoppingAPI.Business_Logic
{
    public class BLHelper
    {
        public static Cache ServerCache;

        static BLHelper()
        {
            ServerCache = new Cache();
        }
    }
}