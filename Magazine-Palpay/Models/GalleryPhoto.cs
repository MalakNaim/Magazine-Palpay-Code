namespace Magazine_Palpay.Web.Models
{
    public class GalleryPhoto :AuthLog
    {
        public int Id { get; set; }
        public int GalleryId { get; set; }
        public Gallery Gallery { get; set; }
        public string Photo { get; set; }
    }
}
