namespace TokenAuthAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
        public string Email { get; set; }
    }
}