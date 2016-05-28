using BeautyTeamWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.ViewModels
{
    public class PrivacyStateViewModel
    {
        public PrivacyStateViewModel() { }
        public PrivacyStateViewModel(ObisoftUser cuser)
        {
            AllowAddtoMyCalendar = cuser.AllowAddtoMyCalendar;
            AllowSeeIfImFree = cuser.AllowSeeIfImFree;
            AllowSeeMyCalendar = cuser.AllowSeeMyCalendar;
            AllowSeeMySchoolAndAccount = cuser.AllowSeeMySchoolAndAccount;
            AllowSeeWhatImDoing = cuser.AllowSeeWhatImDoing;
        }
        /// <summary>
        /// If this is set allow, others may find if I am free right now.
        /// </summary>
        [Required]
        public virtual bool AllowSeeIfImFree { get; set; } = true;
        /// <summary>
        /// If this is set allow, others may see what I am doing right now.
        /// </summary>
        [Required]
        public virtual bool AllowSeeWhatImDoing { get; set; } = false;
        /// <summary>
        /// If this is set allow, others like my boss can add event to my calendar directly.
        /// Even if this is not set allow, Team events or team tasks can also set to my calendar.
        /// </summary>
        [Required]
        public virtual bool AllowAddtoMyCalendar { get; set; } = true;
        /// <summary>
        /// If this is set allow, others can see my full calendar.
        /// </summary>
        [Required]
        public virtual bool AllowSeeMyCalendar { get; set; } = false;
        /// <summary>
        /// If this is set allow, others can see my school and account information.
        /// </summary>
        [Required]
        public virtual bool AllowSeeMySchoolAndAccount { get; set; } = true;
    }
}