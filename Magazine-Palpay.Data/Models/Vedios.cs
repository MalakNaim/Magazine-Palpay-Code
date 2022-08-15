namespace Magazine_Palpay.Data.Models
{
    public class Video :AuthLog
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; } 
        public Post Post { get; set; }

    }
}
