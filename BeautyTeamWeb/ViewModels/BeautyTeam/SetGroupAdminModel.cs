using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.Models
{
    public class SetGroupAdminModel
    {
        [Required]
        public virtual string TargetUserId { get; set; }
    }
}