using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.ViewModels
{
    public class EmailSubscribe
    {
        public virtual int EmailSubscribeId { get; set; }
        [DataType(DataType.EmailAddress)]
        public virtual string Email { get; set; }
    }
}