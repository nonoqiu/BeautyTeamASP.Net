using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.ViewModels
{
    public class HomeIndexViewModel
    {
        public virtual int GroupNumbers { get; set; }
        public virtual int Users { get; set; }
        public virtual int Tasks { get; set; }
        public virtual int Projects { get; set; }
        public virtual IEnumerable<NewsViewModel> Newss { get; set; }
        public virtual IEnumerable<ProductType> Types { get; set; }
        public virtual IEnumerable<Product> Products { get; set; }
    }
    public class SubScribeViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public virtual string Email { get; set; }
    }
}