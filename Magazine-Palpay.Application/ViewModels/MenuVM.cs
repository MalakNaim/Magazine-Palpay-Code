namespace Magazine_Palpay.Application.ViewModels
{
    public class MenuVM :AuthLogVM
    {
        public string Title { get; set; }
        public int Category { get; set; }
        public int? SubCategory { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
        public bool Active { get; set; }
    }
}
