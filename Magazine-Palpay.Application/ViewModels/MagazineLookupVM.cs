namespace Magazine_Palpay.Application.ViewModels
{
    public class MagazineLookupVM : AuthLogVM
    {
        public int LookupId { get; set; }

        public int? LookupChildId { get; set; } 

        public string Name { get; set; }
    }
}
