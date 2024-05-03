using Microsoft.AspNetCore.Mvc;

namespace JTableDemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private static readonly List<Person> _lstPerson = new()
        {
            new Person { R01F01 = 1, R01F02 = "Deep"},
            new Person { R01F01 = 2, R01F02 = "Jeet"},
            new Person { R01F01 = 3, R01F02 = "Shyam"},
            new Person { R01F01 = 4, R01F02 = "Vishal"},
            new Person { R01F01 = 5, R01F02 = "Prince"},
            new Person { R01F01 = 6, R01F02 = "Dev"},
            new Person { R01F01 = 7, R01F02 = "Raj"},
            new Person { R01F01 = 8, R01F02 = "Prajval"},
            new Person { R01F01 = 9, R01F02 = "Isha"},
            new Person { R01F01 = 10, R01F02 = "Disha"},
            new Person { R01F01 = 11, R01F02 = "Harshika"},
            new Person { R01F01 = 12, R01F02 = "Ishika"},
            new Person { R01F01 = 13, R01F02 = "Krinsi"},
            new Person { R01F01 = 14, R01F02 = "Raj"},
            new Person { R01F01 = 15, R01F02 = "Hutish"},
            new Person { R01F01 = 16, R01F02 = "Harshil"}
        };

        [HttpPost]
        public object GetAll(int jtStartIndex = 0, int jtPageSize = 10, string jtSorting = null)
        {
            var query = _lstPerson.AsQueryable();
            if (!string.IsNullOrEmpty(jtSorting))
            {
                string[] sort = jtSorting.Split(' ');
                switch (sort[0])
                {
                    case "R01F01":
                        query = sort[1].ToLower() == "asc" ? query.OrderBy(p => p.R01F01) : query.OrderByDescending(p => p.R01F01);
                        break;
                    case "R01F02":
                        query = sort[1].ToLower() == "asc" ? query.OrderBy(p => p.R01F02) : query.OrderByDescending(p => p.R01F02);
                        break;
                }
            }

            var totalRecordCount = _lstPerson.Count();
            var persons = query.Skip(jtStartIndex).Take(jtPageSize).ToList();

            return new JsonResult(new { Result = "OK", Records = persons, TotalRecordCount = totalRecordCount });
        }

        [HttpPost("Create")]
        public ActionResult<Response> Create(Person person)
        {
            _lstPerson.Add(person);
            return new Response()
            {
                Result = "OK",
                Records = _lstPerson
            };
        }

        [HttpPost("Update")]
        public ActionResult<Response> Update(Person person)
        {
            Person? existingPerson = _lstPerson.FirstOrDefault(p => p.R01F01 == person.R01F01);

            if (existingPerson != null)
            {
                existingPerson.R01F02 = person.R01F02;
                return new Response()
                {
                    Result = "OK",
                };
            }

            return new Response()
            {
                Result = "ERROR",
                Message = "Person not exists"
            };
        }

        [HttpDelete("{id}")]
        public ActionResult<Response> Delete(int id)
        {
            _lstPerson.RemoveAll(p => p.R01F01 == id);
            return new Response()
            {
                Result = "OK",
                Message = "Deleted successfully"
            };
        }
    }

    public class Response
    {
        public string Result { get; set; }
        public dynamic Records { get; set; }
        public string Message { get; set; }
    }
}
