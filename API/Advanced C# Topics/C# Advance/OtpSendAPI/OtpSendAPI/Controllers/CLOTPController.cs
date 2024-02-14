using System;
using System.Net;
using System.Net.Mail;
using System.Web.Caching;
using System.Web.Http;

namespace OtpSendAPI.Controllers
{
    public class CLOTPController : ApiController
    {
        private static Cache _cache;

        static CLOTPController()
        {
            _cache = new Cache();
        }

        [HttpGet]
        [Route("OTP")]
        public IHttpActionResult GenerateOTP(string email)
        {
            string otp = GenerateRandom6Digit();

            SendMail(email, otp);
            _cache.Add(email, otp, null, DateTime.Now.AddMinutes(1), TimeSpan.Zero, CacheItemPriority.Default, null);

            return Ok("Mail sent successfully");
        }

        private void SendMail(string email, string otp)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587);

            smtpClient.Credentials = new NetworkCredential("deeppatel2513@outlook.com", "@Deep2513");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("deeppatel2513@outlook.com", "Deep Patel");
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = "OTP";
            mailMessage.Body = otp;

            smtpClient.Send(mailMessage);
        }

        private string GenerateRandom6Digit()
        {
            Random random = new Random();
            int otp = random.Next(0, 999999);
            return otp.ToString("000000");
        }
    }
}
