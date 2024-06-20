using DataGridFinalDemoAPI.Business_Logic.Interfaces;
using DataGridFinalDemoAPI.Models;

namespace DataGridFinalDemoAPI.Business_Logic.Handlers
{
    public class BLSTU01Handler : ISTU01Service
    {
        private readonly List<STU01> _lstSTU01;
        private int _nextId;

        public BLSTU01Handler()
        {
            _nextId = 1;
            _lstSTU01 = FillSTU01List(1000);
        }

        public bool Add(STU01 objSTU01)
        {
            objSTU01.U01F01 = _nextId++;
            _lstSTU01.Add(objSTU01);

            return true;
        }

        public bool Delete(int id)
        {
            _lstSTU01.RemoveAll(stu => stu.U01F01 == id);
            return true;
        }

        public List<STU01> GetAll()
        {
            return _lstSTU01;
        }

        public bool Update(STU01 objSTU01)
        {
            STU01? existingSTU01 = _lstSTU01.Find(stu => stu.U01F01 == objSTU01.U01F01);
            existingSTU01 = objSTU01;
            return true;
        }

        public bool ValidateMobileNumber(int id, string number)
        {
            return !_lstSTU01.Any(stu => stu.U01F05.Equals(number) && stu.U01F01 != id);
        }

        private List<STU01> FillSTU01List(int studentCount)
        {
            Random rand = new();
            List<string> firstNames = new() { "Aarav", "Vivaan", "Aditya", "Vihaan", "Arjun", "Sai", "Reyansh", "Ayaan", "Krishna", "Ishaan" };
            List<string> lastNames = new() { "Patel", "Sharma", "Mehta", "Trivedi", "Joshi", "Desai", "Amin", "Rao", "Kumar", "Singh" };
            List<STU01> lstSTU01 = new();

            for (int i = 1; i <= studentCount; i++)
            {
                string firstName = firstNames[rand.Next(firstNames.Count)];
                string lastName = lastNames[rand.Next(lastNames.Count)];
                string fullName = $"{firstName} {lastName}";
                int age = rand.Next(18, 25); // Random age between 18 and 25
                string email = $"{firstName.ToLower()}.{lastName.ToLower()}@example.com";
                string mobile = $"{rand.Next(6, 9)}{rand.Next(100000000, 999999999)}"; // Random 10-digit mobile number
                int cityId = rand.Next(1, 5); // Random city ID between 1 and the number of cities

                lstSTU01.Add(new STU01
                {
                    U01F01 = _nextId++,
                    U01F02 = fullName,
                    U01F03 = age,
                    U01F04 = email,
                    U01F05 = mobile,
                    U01F06 = cityId
                });
            }

            return lstSTU01;
        }
    }
}
