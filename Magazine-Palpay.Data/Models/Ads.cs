using System;

namespace Magazine_Palpay.Data.Models
{
    public class Ads :AuthLog
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Desription { get; set; }

        public string Image { get; set; }

        public DateTime? StatDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Order { get; set; }
    }
}
