namespace Magazine_Palpay.Web.Models
{
    public class PostPhoto : AuthLog
    {
        public int Id { get; set; } 

        public int PostId { get; set; }

        public string Photo { get; set; }

        public Post Post { get; set; }
    }
}
