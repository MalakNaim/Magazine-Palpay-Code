using System.Collections.Generic;

namespace Magazine_Palpay.Data.Models
{
    public class Post : AuthLog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PostType { get; set; }
        public int? PostSubType { get; set; }
        public string Description { get; set; }
        public string MainImage { get; set; }
        public ICollection<PostPhoto> PostPhotos { get; set; }
    }
}
