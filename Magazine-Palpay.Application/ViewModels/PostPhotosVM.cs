namespace Magazine_Palpay.Application.ViewModels
{
    public class PostPhotosVM : AuthLogVM
    {
        public int Id { get; set; } 

        public int PostId { get; set; }

        public string Photo { get; set; }

        public PostVM Post { get; set; }
    }
}
