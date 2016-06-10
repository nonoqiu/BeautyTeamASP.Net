using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeautyTeamWeb.Models
{
    /// <summary>
    /// Gives definition of a Group
    /// </summary>
    public abstract class Group
    {
        public virtual int GroupId { get; set; }
        public virtual string GroupName { get; set; }
        public virtual string GroupDescription { get; set; }
        public virtual GroupType GroupType { get; set; }
        [DataType(DataType.ImageUrl)]
        public virtual string GroupImage { get; set; }
        public virtual List<GU_Relation> GU_Relations { get; set; }
        public virtual bool CanNotBeSearched { get; set; } = false;
        public virtual DateTime CreateTime { get; set; } = DateTime.Now;
        public abstract string Type { get; }
        [JsonIgnore]
        public virtual List<Invitation> Invitations { get; set; }
        [JsonIgnore]
        public virtual List<GroupPosts> GroupPostss { get; set; }
        [JsonIgnore]
        public virtual ObisoftUser Creater => this.GU_Relations.Find(t => t.RelationType == GU_RelationType.Owner).ObisoftUser;
    }
}