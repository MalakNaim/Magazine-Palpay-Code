using System;

namespace Magazine_Palpay.Data.Models
{
    public class AuthLog
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public bool Deleted { get; set; } = false;
    }
}
