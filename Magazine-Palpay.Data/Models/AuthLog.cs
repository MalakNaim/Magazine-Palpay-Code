using System;

namespace Magazine_Palpay.Data.Models
{
    public class AuthLog
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
          public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDelete { get; set; } = false;
    }
}
