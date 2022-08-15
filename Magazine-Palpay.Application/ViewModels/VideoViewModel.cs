namespace Magazine_Palpay.Application.ViewModels
{
    public class VideoViewModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; } 
        public PostViewModel Post { get; set; }

    }
}
