using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Magazine_Palpay.Data.Models
{
    public class Post : AuthLog
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="أدخل عنوان المنشور")]
        public string Head { get; set; }
        [Required(ErrorMessage = "أدخل نص المنشور")]
        public string Body { get; set; }
        [Required(ErrorMessage = "أدخل نوع المنشور")]
        public int PostTypeId { get; set; }
        public int? PostSubType { get; set; }
        public string MainImage { get; set; }
        public bool PublishedPost { get; set; }
        [Required(ErrorMessage = "أدخل ترتيب المنشور")]
        public int OrderPlace { get; set; }
        public PostType PostType { get; set; }
        public ICollection<PostPhoto> PostPhoto { get; set; }
    }
}
