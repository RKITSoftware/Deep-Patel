namespace IdentityDemo.Interfaces
{
    public interface ISendEmail
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
