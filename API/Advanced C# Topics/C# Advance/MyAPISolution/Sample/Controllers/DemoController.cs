using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Sample.Controllers
{
    public class DemoController : ApiController
    {
        public IHttpActionResult Get()
        {
            int a = 1;
            int b = 10 / a;

            return Ok(b);
        }

        public IHttpActionResult Post()
        {
            return Ok("Done");
        }

        public async Task<IHttpActionResult> DeleteAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync("https://www.nseindia.com/");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return Ok(responseBody);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return NotFound();
        }
    }
}
