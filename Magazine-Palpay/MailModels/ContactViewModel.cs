using Microsoft.AspNetCore.Http;

namespace Magazine_Palpay.MailModels
{
    public class ContactViewModel
    {
        public string Name { get; set; }

        public string Subject { get; set; }

        public string Email { get; set; }

        public string Body { get; set; }

        public IFormFile Attachment { get; set; }
    }
}
