using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.ViewModels
{
    public enum JobType : int
    {
        Art = 0,
        Engineering = 1,
        Production = 2,
        Operations = 3
    }
    public class JobOpening
    {
        public virtual int JobOpeningId { get; set; }
        public virtual string JobOpeningName { get; set; }

        [DataType(DataType.Html)]
        public virtual string JobDescription { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]

        public virtual DateTime PublishTime { get; set; }

        public virtual JobType JobType { get; set; }

        public virtual int Views { get; set; }
    }
}