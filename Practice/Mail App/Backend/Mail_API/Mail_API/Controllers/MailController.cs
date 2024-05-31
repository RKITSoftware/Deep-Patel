using Mail_API.Data;
using Mail_API.Dtos;
using Mail_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Mail_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MailController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MailController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("sent")]
        public async Task<IActionResult> SentMail(SendMailDto sendMailDto)
        {
            string senderId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            User receiver = _context.Users.First(u => u.Username.Equals(sendMailDto.ReceiverUsername));

            Mail mail = new()
            {
                SenderId = Convert.ToInt32(senderId),
                ReceiverId = receiver.Id,
                Subject = sendMailDto.Subject,
                Body = sendMailDto.Body,
                SentDate = DateTime.UtcNow,
            };

            mail.Sender = _context.Users.First(u => u.Id == Convert.ToInt32(senderId));
            mail.Receiver = receiver;

            _context.Mails.Add(mail);
            await _context.SaveChangesAsync();

            return Ok("Successfully sent");
        }

        [HttpGet("all")]
        public async Task<ActionResult<Response>> GetMyMails()
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var mails = _context.Mails.Where(mail => mail.ReceiverId == Convert.ToInt32(userId)).OrderByDescending(m => m.SentDate).Include(a => a.Sender).Include(b => b.Receiver);

            return Ok(mails);
        }
    }
}
