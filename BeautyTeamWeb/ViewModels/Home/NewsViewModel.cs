using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.ViewModels
{
    public class NewsViewModel
    {
        public virtual int NewsViewModelId { get; set; }

        public virtual string NewsViewModelName { get; set; }

        [DataType(DataType.Html)]
        public virtual string NewsContent { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]

        public virtual DateTime PublishTime { get; set; }

        public virtual int Views { get; set; } = 0;

        public virtual string ToAction { get; set; } = string.Empty;
        public virtual string ToController { get; set; } = string.Empty;

        [DataType(DataType.ImageUrl)]
        public virtual string ImageURL { get; set; } = string.Empty;
    }
}