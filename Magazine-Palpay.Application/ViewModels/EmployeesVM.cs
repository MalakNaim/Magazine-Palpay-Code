using System;

namespace Magazine_Palpay.Application.ViewModels
{
    public class EmployeesVM :AuthLogVM
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public int Department { get; set; }
        public int LookupId { get; set; }
        public string JobTitle { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
