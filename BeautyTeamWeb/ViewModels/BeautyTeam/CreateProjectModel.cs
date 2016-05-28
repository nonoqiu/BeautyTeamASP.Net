using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.Models
{
    public class CreateProjectModel
    {
        [Required]
        public virtual string ProjectName { get; set; }
        public virtual string ProjectDescription { get; set; }
    }
}