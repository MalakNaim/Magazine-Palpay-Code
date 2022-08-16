﻿namespace Magazine_Palpay.Application.Features.MagazineSetting.Commands
{
    public class UpdateMagazineSettingCommand
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Logo { get; set; }
        public string LogoDark { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Website { get; set; }
        public string Facebook { get; set; }
        public string Instgram { get; set; }
        public string LinkedIn { get; set; }
        public string Twitter { get; set; }
        public string OtherUrls { get; set; }
    }
}