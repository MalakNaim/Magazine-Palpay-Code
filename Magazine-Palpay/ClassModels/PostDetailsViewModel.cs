using Magazine_Palpay.Web.Models;
using System.Collections.Generic;

namespace Magazine_Palpay.ClassModels
{
    public class PostDetailsViewModel
    {
        public Post Post { get; set; }

        public List<Post> Posts { get; set; }

        public Ads Ads { get; set; }
    }
}
