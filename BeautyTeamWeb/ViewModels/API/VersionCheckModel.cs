using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.Models
{
    public class VersionCheckModel
    {
        [Required]
        public string Platform { get; set; }
        public string CurrentVersion { get; set; }
    }
    public class VersionCheckResult:ServerReply
    {
        public string LatestVersion { get; set; }
        public string LatestVersionDescription { get; set; }
        public bool IsLatest { get; set; }
    }
}