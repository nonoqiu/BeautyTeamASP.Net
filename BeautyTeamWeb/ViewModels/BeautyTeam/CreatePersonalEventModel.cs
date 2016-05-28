using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.Models
{
    public class CreatePersonalEventModel
    {
        [Required]
        public virtual string EventName { get; set; }
        /// <summary>
        /// This is not required!
        /// </summary>
        public virtual string EventContent { get; set; }
        /// <summary>
        /// This is not required. If Client did not give server(gives server null), server will set 5 days later.
        /// </summary>
        public virtual DateTime? HappenTime { get; set; }
        /// <summary>
        /// This is not required. If Client did not give server(gives server null), server will set 6 days later.
        /// </summary>
        public virtual DateTime? EndTime { get; set; }
        /// <summary>
        /// This is not required. If Client did not give server(gives server null), server will set 25 minutes before deadline.
        /// Can not be 1 day or longer!!!
        /// </summary>
        public virtual TimeSpan? NoticeBefore { get; set; }
    }
}