using System;

namespace Magazine_Palpay.Web.Models
{
    public class Ads :AuthLog
    {
        public int Id { get; set; }

        public string Head { get; set; }

        public string Body { get; set; }

        public string Owner { get; set; }

        public string Link { get; set; }

        public string Image { get; set; }

        public DateTime? StatDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Order { get; set; }
    }
}
