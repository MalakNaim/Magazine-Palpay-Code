namespace Magazine_Palpay.Data.Models
{
    public class Gallery :AuthLog
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Photo { get; set; }
    }
}
