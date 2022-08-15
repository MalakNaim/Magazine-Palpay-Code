namespace Magazine_Palpay.Application.ViewModels
{
    public class PostPhotoViewModel
    {
        public int Id { get; set; } 

        public int PostId { get; set; }

        public string Photo { get; set; }

        public PostViewModel Post { get; set; }
    }
}
