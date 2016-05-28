using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.ViewModels
{
    public class Bind
    {
        [Required]
        public virtual int school { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public virtual string email { get; set; }
        [Required]
        public virtual string aaoaccount { get; set; }
        [Required]
        public virtual string aaopass { get; set; }
        [Required]
        public virtual string openid { get; set; }
    }
}