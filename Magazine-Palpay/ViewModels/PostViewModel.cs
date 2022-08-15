using System.Collections.Generic;

namespace Magazine_Palpay.Web.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; } 
        public string Head { get; set; }
        public string Body { get; set; }
        public int PostTypeId { get; set; }
        public int? PostSubType { get; set; }
        public string MainImage { get; set; }
        public PostTypeViewModel PostType { get; set; }
        public List<PostPhotoViewModel> PostPhotos { get; set; }
    }
}
