using System.Collections.Generic;

namespace Magazine_Palpay.Data.Models
{
    public class Post : AuthLog
    {
        public int Id { get; set; } 
        public string Head { get; set; }
        public string Body { get; set; }
        public int PostTypeId { get; set; }
        public int? PostSubType { get; set; }
        public string MainImage { get; set; }
        public bool IsPublished { get; set; }
        public int Order { get; set; }
        public PostType PostType { get; set; }
        public ICollection<PostPhoto> PostPhoto { get; set; }
    }
}
