using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.Models
{
    public class CreatePersonalTaskModel
    {
        [Required]
        public virtual string TaskName { get; set; }
        /// <summary>
        /// This is not required!
        /// </summary>
        public virtual string TaskContent { get; set; }
        /// <summary>
        /// This is not required. If Client did not give server(gives server null), server will set 5 days later.
        /// </summary>
        public virtual DateTime? DeadLine { get; set; }
        /// <summary>
        /// This is not required. If Client did not give server(gives server null), server will set 24 hours before deadline.
        /// Can not be 1 day or longer!!!
        /// </summary>
        public virtual TimeSpan? NoticeBefore { get; set; }
    }
}