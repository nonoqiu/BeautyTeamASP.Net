using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.Models
{
    public enum Platform : short
    {
        Android = 0,
        iOS = 1,
        Web = 2,
        WindowsPhone = 3,
        Windows = 4
    }
}