using MiddleWareDemo.Model;

namespace MiddleWareDemo.Business_Logic
{
    public class BLUser
    {
        private static List<USR01> _lstUsers;

        static BLUser()
        {
            _lstUsers = new List<USR01>();
        }

        public void Add(USR01 objUser)
        {
            _lstUsers.Add(objUser);
        }

        public void Delete(int id)
        {
            _lstUsers.RemoveAll(usr => usr.R01F01 == id);
        }

        public IEnumerable<USR01> Get()
        {
            return _lstUsers;
        }

        public USR01 Get(int id)
        {
            return _lstUsers.FirstOrDefault(usr => usr.R01F01 == id);
        }
    }
}
