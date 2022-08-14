using System;

namespace Magazine_Palpay.Application.ViewModels
{
    public class AuthLogVM
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
    }
}
