﻿namespace Magazine_Palpay.Web.ViewModels
{
    public class GalleryPhotosViewModel
    {
        public int Id { get; set; }
        public int GalleryId { get; set; }
        public string Photo { get; set; }
        public GalleryViewModel Gallery { get; set; }

    }
}