﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Magazine_Palpay.Web.Models
{
    public class Post : AuthLog
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="أدخل عنوان المنشور")]
        public string Head { get; set; }
        public string Body { get; set; }
        [Required(ErrorMessage = "أدخل نوع المنشور")]
        public int PostTypeId { get; set; }
        [Required(ErrorMessage = "أدخل نوع الميديا المستخدمة")]
        public int MediaType { get; set; }
        public int? PostSubTypeId { get; set; }
        public string MainImage { get; set; }
        public string VideoLink { get; set; }
        public string EmbedVideoLink { get; set; }
        public bool PublishedPost { get; set; }
        [Required(ErrorMessage = "أدخل ترتيب المنشور")]
        public int OrderPlace { get; set; }
        public PostType PostType { get; set; }
        public ICollection<PostPhoto> PostPhoto { get; set; }
    }
}