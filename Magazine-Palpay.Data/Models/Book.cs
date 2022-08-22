namespace Magazine_Palpay.Data.Models
{
    public class Book :AuthLog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BookCategoryId { get; set; }
        public string Author { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
        public BookCategory BookCategory { get; set; }
    }
}
