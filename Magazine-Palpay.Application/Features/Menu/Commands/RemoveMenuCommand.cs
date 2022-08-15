
namespace Magazine_Palpay.Application.Features.Menu.Commands
{
    public class RegisterMenuCommand
    {
        public string Title { get; set; }
        public int Category { get; set; }
        public int? SubCategory { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
        public bool Active { get; set; }
    }
}
