namespace Magazine_Palpay.Data.Models
{
    public class MagazineLookup : AuthLog
    {
        public int Id { get; set; }

        public int LookupId { get; set; }

        public int? LookupChildId { get; set; } 

        public string Name { get; set; }
    }
}
