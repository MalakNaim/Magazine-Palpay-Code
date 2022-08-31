﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazine_Palpay.Web.Models
{
    public class PostType : AuthLog
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public ICollection<Post> Post { get; set; }
    }
}