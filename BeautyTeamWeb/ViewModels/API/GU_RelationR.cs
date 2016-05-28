using BeautyTeamWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.ViewModels
{
    public class GU_RelationR
    {
        public virtual int GU_RelationId { get; set; }
        public virtual string ObisoftUserId { get; set; }
        [JsonIgnore]
        public virtual ObisoftUser ObisoftUser { get; set; }
        public virtual int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public virtual GU_RelationType RelationType { get; set; } = GU_RelationType.Member;
        public GU_RelationR(GU_Relation G)
        {
            ObisoftUser = G.ObisoftUser;
            ObisoftUserId = G.ObisoftUserId;
            GU_RelationId = G.GU_RelationId;
            GroupId = G.GroupId;
            Group = G.Group;
            RelationType = G.RelationType;
            CreaterName = G.Group.Creater.NickName;
            CreaterID = G.Group.Creater.Id;
        }
        public virtual string CreaterName { get; set; }
        public virtual string CreaterID { get; set; }
    }
}