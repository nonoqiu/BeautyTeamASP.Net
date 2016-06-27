using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.ViewModels
{
    public class Product
    {
        public virtual int ProductId { get; set; }
        public virtual string ProductName { get; set; }
        [DataType(DataType.ImageUrl)]
        public virtual string ImageUrl { get; set; }
        public virtual string ControllerName { get; set; }
        public virtual string ActionName { get; set; }
        public virtual int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual string ShortDescription { get; set; }
        public virtual string Description { get; set; }
    }
}