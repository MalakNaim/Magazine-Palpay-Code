using System.Collections.Generic;

namespace Magazine_Palpay.Web.Models
{
    public class BookCategory :AuthLog
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Book { get; set; }
    }
}
