using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.Models
{
    public class Invitation
    {
        public virtual int InvitationId { get; set; }

        public virtual int GroupId { get; set; }
        public virtual Group Group { get; set; }

        public virtual string ObisoftUserId { get; set; }
        public virtual ObisoftUser ObisoftUser { get; set; }
    }
}