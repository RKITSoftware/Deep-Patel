namespace Mail_API.Dtos
{
    public class SendMailDto
    {
        public string ReceiverUsername { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
