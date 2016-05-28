using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.ViewModels
{
    public class UploadImageViewModel
    {
        [Required]
        public bool UnCompressed { get; set; }
        [Required]
        public bool HTTPS { get; set; }
    }
}