using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.Models
{
    public class CreateRadioStationModel
    {
        [Required]
        public string RadioStationName { get; set; }
        public string RadioStationDescription { get; set; }
        [Required]
        public GroupType GroupType { get; set; }
    }
}