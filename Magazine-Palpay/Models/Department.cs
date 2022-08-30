using System.Collections.Generic;

namespace Magazine_Palpay.Web.Models
{
    public class Department :AuthLog
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Employee> Employee { get; set; }
    }
}
