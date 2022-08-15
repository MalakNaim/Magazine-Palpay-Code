using System;
using System.ComponentModel.DataAnnotations;

namespace Magazine_Palpay.Web.ViewModels
{
    public class PostTypeViewModel
    {
        public int Id { get; set; }
      
        [Required(ErrorMessage ="يرجى إدخال النوع")]
        [Display(Name="النوع")]
        public string Name { get; set; }
        public string Parent { get; set; }

        [Display(Name = "تابع للنوع")]
        public int ParentId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
