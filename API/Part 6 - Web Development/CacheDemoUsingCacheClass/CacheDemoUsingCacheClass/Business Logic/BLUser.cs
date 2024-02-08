using CacheDemoUsingCacheClass.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Caching;

namespace CacheDemoUsingCacheClass.Business_Logic
{
    /// <summary>
    /// Business Logic class for managing User data with caching.
    /// </summary>
    public class BLUser
    {
        // Static list of users and cache instance to store user data.
        private static List<User> _lstUser;
        private static Cache _cache;

        // Static constructor to initialize static members.
        static BLUser()
        {
            _cache = new Cache();
            _lstUser = new List<User>()
            {
                new User {Id = 1, Name = "Deep", Age = 21},
                new User {Id = 2, Name = "Prajval", Age = 21},
                new User {Id = 3, Name = "Vishal", Age = 21},
                new User {Id = 4, Name = "Harshika", Age = 21},
                new User {Id = 5, Name = "Shyam", Age = 21},
            };
        }

        /// <summary>
        /// Retrieves the list of users, either from cache or initializes and caches it.
        /// </summary>
        /// <returns>The list of users.</returns>
        public List<User> GetUsers()
        {
            // Attempt to retrieve users from cache.
            List<User> users = _cache.Get("lstUser") as List<User>;

            // If users are found in the cache, return them.
            if (users != null)
            {
                return users;
            }

            // Simulate a delay (e.g., database query) to emphasize the caching benefit.
            Thread.Sleep(5000);

            // Set cache expiration time to 10 seconds.
            TimeSpan ts = new TimeSpan(0, 0, 10);

            // Add the list of users to cache with a specified expiration time.
            _cache.Add("lstUser", _lstUser, null, DateTime.MaxValue, ts, CacheItemPriority.Default, null);

            // Return the list of users.
            return _lstUser;
        }
    }
}
