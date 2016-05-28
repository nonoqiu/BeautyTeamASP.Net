using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.Models
{

    public class EventLog
    {
        [Required]
        public DateTime HappenTime { get; set; }
        public string Description { get; set; }
        [Required]
        public Platform HappenPlatform { get; set; }
        [Required]
        public string version { get; set; }
    }

}