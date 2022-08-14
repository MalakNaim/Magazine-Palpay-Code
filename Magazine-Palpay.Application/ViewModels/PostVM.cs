using System.Collections.Generic;

namespace Magazine_Palpay.Application.ViewModels
{
    public class PostVM : AuthLogVM
    {
        public string Title { get; set; }
        public int PostType { get; set; }
        public int? PostSubType { get; set; }
        public string Description { get; set; }
        public string MainImage { get; set; }
        public List<PostPhotosVM> PostPhotos { get; set; }
    }
}
