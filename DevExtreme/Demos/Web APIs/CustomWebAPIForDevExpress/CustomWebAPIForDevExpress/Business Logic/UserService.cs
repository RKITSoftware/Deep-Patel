using CustomWebAPIForDevExpress.Models;

namespace CustomWebAPIForDevExpress.Business_Logic
{
    public class UserService
    {
        private readonly List<User> _lstUsers;
        private readonly List<Job> _lstJobs;
        private int _nextUserId = 101;

        public UserService()
        {
            _lstJobs = new List<Job>()
            {
                new Job()
                {
                    Id = 1,
                    JobName = "Full Stack Developer"
                },
                new Job()
                {
                    Id = 2,
                    JobName = "QA"
                },
                new Job()
                {
                    Id = 3,
                    JobName = "Designer"
                }
            };

            _lstUsers = GenerateUserList(100);
        }

        public IQueryable<User> GetAll()
        {
            return _lstUsers.AsQueryable();
        }

        public bool UpdateDetails(User objUser)
        {
            User? user = _lstUsers.Find(u => u.Id == objUser.Id);

            if (user == null)
            {
                return false;
            }

            user.Name = string.IsNullOrEmpty(objUser.Name) ? user.Name : objUser.Name;
            user.Gender = string.IsNullOrEmpty(objUser.Gender) ? user.Gender : objUser.Gender;
            user.Number = objUser.Number > 0 ? objUser.Number : user.Number;
            user.JobId = objUser.JobId > 0 ? objUser.JobId : user.JobId;
            return true;
        }

        public bool AddUser(User user)
        {
            user.Id = _nextUserId++;
            _lstUsers.Add(user);
            return true;
        }

        private List<User> GenerateUserList(int count)
        {
            var users = new List<User>();
            var random = new Random();

            for (int i = 1; i <= count; i++)
            {
                users.Add(new User
                {
                    Id = i,
                    Name = $"User {i}",
                    Gender = random.Next(0, 2) == 0 ? "Male" : "Female",
                    Number = random.Next(1000, 9999),
                    JobId = _lstJobs[random.Next(_lstJobs.Count)].Id
                });
            }

            return users;
        }

        public bool Delete(int id)
        {
            _lstUsers.RemoveAll(u => u.Id == id);
            return true;
        }

        public List<Job> GetAllJobs()
        {
            return _lstJobs;
        }

        public Job GetJob(int id)
        {
            return _lstJobs.Find(j => j.Id == id);
        }
    }
}
