using System;
using System.ComponentModel.DataAnnotations;

namespace Magazine_Palpay.Data.Models
{
    public class Employee :AuthLog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public int? DepartmentId { get; set; }
        public string JobTitle { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? JoinDate { get; set; } 
        [Required(ErrorMessage ="يرجى إدخال المركز")]
        public int Order { get; set; }
        public Department Department { get; set; }

    }
}
