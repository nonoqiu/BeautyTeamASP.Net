using Newtonsoft.Json;

namespace BeautyTeamWeb.Models
{
    public enum GU_RelationType : short
    {
        Owner = 1,
        Admin = 2,
        Member = 3
    }
    public enum GroupType : int
    {
        Others = 1,
        Work = 2,
        Organization = 3,
        Family = 4,
        Private = 5,
        Classmates = 6
    }
    /// <summary>
    /// Describe which user belongs to which group.
    /// </summary>
    public class GU_Relation
    {
        public virtual int GU_RelationId { get; set; }

        public virtual string ObisoftUserId { get; set; }
        [JsonIgnore]
        public virtual ObisoftUser ObisoftUser { get; set; }

        public virtual int GroupId { get; set; }
        [JsonIgnore]
        public virtual Group Group { get; set; }

        public virtual GU_RelationType RelationType { get; set; } = GU_RelationType.Member;
    }

    /// <summary>
    /// Gives relation describes that a task belongs to which person in the team.
    /// </summary>
    public class TU_Relation
    {
        public virtual int TU_RelationId { get; set; }

        public virtual int GroupTaskTaskId { get; set; }
        [JsonIgnore]
        public virtual GroupTask GroupTask { get; set; }

        public virtual string ObisoftUserId { get; set; }
        [JsonIgnore]
        public virtual ObisoftUser ObisoftUser { get; set; }
    }
    /// <summary>
    /// Gives relation decribes that a event belongs to which person in the team.
    /// </summary>
    public class EU_Relation
    {
        public virtual int EU_RelationId { get; set; }

        public virtual int GroupEventEventId { get; set; }
        [JsonIgnore]
        public virtual GroupEvent GroupEvent { get; set; }

        public virtual string ObisoftUserId { get; set; }
        [JsonIgnore]
        public virtual ObisoftUser ObisoftUser { get; set; }
    }
}