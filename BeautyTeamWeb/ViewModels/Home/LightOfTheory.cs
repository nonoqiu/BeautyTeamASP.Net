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
        public string TeamName { get; set; }
        [Phone]
        public string  Phone { get; set; }
        [DataType(dataType:DataType.EmailAddress)]
        public string Email { get; set; }
        public string Class { get; set; }
        public string Messages { get; set; }
    }
}