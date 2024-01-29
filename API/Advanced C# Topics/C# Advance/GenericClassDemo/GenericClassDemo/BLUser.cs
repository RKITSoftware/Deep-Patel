using System.Collections.Generic;
using System.Linq;

namespace GenericClassDemo
{
    public class BLUser : IBLService<USR01>
    {
        private static List<USR01> lstUser = new List<USR01>();

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">User id</param>
        public void Delete(int id)
        {
            lstUser.RemoveAll(usr => usr.R01F01 == id);
        }

        /// <summary>
        /// Creating a user
        /// </summary>
        /// <param name="data">User data</param>
        public void Create(USR01 data)
        {
            lstUser.Add(data);
        }

        /// <summary>
        /// Getting all user information
        /// </summary>
        /// <returns>List of user data</returns>
        public List<USR01> GetAllData()
        {
            return lstUser;
        }

        /// <summary>
        /// Get user data by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User</returns>
        public USR01 GetData(int id)
        {
            return lstUser.FirstOrDefault(user => user.R01F01 == id);
        }

        /// <summary>
        /// Update the user information using new user data
        /// </summary>
        /// <param name="data">Updated data to replace data of user</param>
        public void Update(USR01 data)
        {
            USR01 objUser = lstUser.FirstOrDefault(user => user.R01F01 == data.R01F01);
            objUser.R01F02 = data.R01F02;
        }
    }
}
