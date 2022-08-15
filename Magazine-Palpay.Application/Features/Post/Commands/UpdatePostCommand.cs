﻿namespace Magazine_Palpay.Application.Features.Post.Commands
{
    public class UpdatePostCommand
    {
        public int Id { get; set; }
        public string Head { get; set; }
        public string Body { get; set; }
        public int PostTypeId { get; set; }
        public int? PostSubType { get; set; }
        public string MainImage { get; set; }
    }
}
