using BeautyTeamWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.ViewModels
{
    public class LightOfTheory
    {
        public virtual int LightOfTheoryId { get; set; }
        public string ObisoftUserId { get; set; }
        public ObisoftUser  ObisoftUser { get; set; }
        [Required]
        public string YourName { get; set; }
        [Required]
        public string TeamMembers { get; set; }
        [Required]
        [Phone]
        public string  Phone { get; set; }
        [DataType(dataType:DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Class { get; set; }
        [Required]
        public string Messages { get; set; }
    }
}