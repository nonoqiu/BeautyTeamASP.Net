using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.Models
{

    public class FriendsPart
    {
        public virtual int FriendsPartId { get; set; }

        public virtual List<FU_Relation> Friends { get; set; } = new List<FU_Relation>();
        public virtual string PartName { get; set; }
        //Parent
        public virtual string ParentId { get; set; }
        [JsonIgnore]
        public virtual ObisoftUser Parent { get; set; }
    }
    public class FU_Relation
    {
        public virtual int FU_RelationId { get; set; }
        //Parent
        public virtual int ParentId { get; set; }
        [JsonIgnore]
        public virtual FriendsPart Parent { get; set; }

        public virtual string FriendId { get; set; }
        [JsonIgnore]
        public virtual ObisoftUser Friend { get; set; }
    }
}