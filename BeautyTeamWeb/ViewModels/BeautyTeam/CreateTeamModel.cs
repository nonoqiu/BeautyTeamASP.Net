using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.Models
{
    public class CreateTeamModel
    {
        [Required]
        public string TeamName { get; set; }
        public string TeamDescription { get; set; }
        [Required]
        public GroupType GroupType { get; set; }
    }
}