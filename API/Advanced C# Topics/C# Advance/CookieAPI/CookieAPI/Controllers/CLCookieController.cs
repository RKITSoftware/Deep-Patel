using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace CookieAPI.Controllers
{
    [RoutePrefix("api/CLCookie")]
    public class CLCookieController : ApiController
    {
        [HttpGet]
        [Route("SetCookie")]
        public HttpResponseMessage SetCookie()
        {
            //BLSession obj = new BLSession();
            //string ans = obj.CreateSessionID(HttpContext.Current);

            //System.Diagnostics.Debug.WriteLine(HttpContext.Current.Session.SessionID);
            // Single Cookie Add

            var response = new HttpResponseMessage();
            var cookie = new CookieHeaderValue("session-id", "123");
            cookie.Expires = DateTime.Now.AddDays(1);
            cookie.Path = "/";

            response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            return response;

            // Multiple Cookie Add in single 
            //var response = new HttpResponseMessage();
            //var value = new NameValueCollection();
            //value["sid"] = "123";
            //value["token"] = "Basic";
            //value["theme"] = "Light";

            //var cookie = new CookieHeaderValue("session", value);
            //cookie.Expires = DateTime.Now.AddSeconds(30);
            //response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            //return response;
        }

        [HttpGet]
        [Route("GetCookie")]
        public IHttpActionResult GetCookieValue()
        {
            CookieHeaderValue cookie = Request.Headers.GetCookies("session").FirstOrDefault();

            if (cookie != null)
            {
                // return Ok(cookie["session-id"].Value);

                CookieState cookieState = cookie["session"];
                return Ok(cookieState.Values["Theme"]);
            }

            return NotFound();
        }
    }
}
