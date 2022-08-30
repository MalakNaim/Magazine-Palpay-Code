using System.ComponentModel.DataAnnotations;

namespace Magazine_Palpay.Web.Models
{
    public class LastNews : AuthLog
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "يرجى إدخال نص الخبر")]
        public string Title { get; set; }
    }
}
