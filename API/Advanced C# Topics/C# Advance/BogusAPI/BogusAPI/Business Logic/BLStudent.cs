using Bogus;
using BogusAPI.Models;
using System.Collections.Generic;

namespace BogusAPI.Business_Logic
{
    public class BLStudent
    {
        public static List<STU01> FakeData()
        {
            // Create a Faker instance for generating fake data
            var faker = new Faker<STU01>("en_IND")
             .RuleFor(x => x.Id, f => f.IndexFaker + 1)
             .RuleFor(x => x.FirstName, f => f.Person.FirstName)
             .RuleFor(x => x.LastName, f => f.Person.LastName)
             .RuleFor(x => x.FullName, (f, u) => $"{u.FirstName} {u.LastName}")
             .RuleFor(x => x.Gender, f => f.Person.Gender.ToString())
             .RuleFor(x => x.Occupations, f => f.Random.Word())
             .RuleFor(x => x.ProfilePic, f => f.Internet.Avatar());

            // Generate a list of fake STU01 objects
            List<STU01> fakeData = faker.Generate(10); // You can specify the number of items you want

            return fakeData;
        }
    }
}