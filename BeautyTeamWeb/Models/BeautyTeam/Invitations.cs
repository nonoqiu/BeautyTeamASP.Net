using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.Models
{
    public abstract class Invitation
    {
        public virtual int InvitationId { get; set; }

        //Target
        public virtual string ObisoftUserId { get; set; }
        public virtual ObisoftUser ObisoftUser { get; set; }
    }
    //Person to person
    public class P2PInvitation : Invitation
    {
        //From
        public virtual int FriendsPartId { get; set; }
        public virtual FriendsPart FriendsPart { get; set; }
    }
    //Invite to Group
    public class G2PInvitation : Invitation
    {
        //From
        public virtual int GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}