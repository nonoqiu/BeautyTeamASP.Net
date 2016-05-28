using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.ViewModels
{
    public class BasicInfoViewModel
    {
        public virtual string PhoneNumber { get; set; }
        /// <summary>
        /// Nickname of the user.
        /// </summary>
        public virtual string NickName { get; set; }
        /// <summary>
        /// Real Name of the user.
        /// </summary>
        public virtual string RealName { get; set; }
        /// <summary>
        ///The address of the user's icon
        /// </summary>
        [DataType(DataType.ImageUrl)]
        public virtual string IconImage { get; set; }
        ///<summary>
        ///His personal description, not more than 150 length.
        ///</summary>
        [MaxLength(150)]
        public virtual string Description { get; set; } = "Nothing to say.";
    }
}