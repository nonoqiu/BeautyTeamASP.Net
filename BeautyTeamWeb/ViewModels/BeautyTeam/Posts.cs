using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.ViewModels
{
    public abstract class PostsViewModel
    {
        [Required]
        [MaxLength(40)]
        public virtual string Title { get; set; }
        [DataType(DataType.Html)]
        [Required]
        [MaxLength(65535)]
        public virtual string Content { get; set; }
    }
}