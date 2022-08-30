namespace Magazine_Palpay.Web.Models
{
    public class Menu :AuthLog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Category { get; set; }
        public int? SubCategory { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
        public bool Active { get; set; }
    }
}
