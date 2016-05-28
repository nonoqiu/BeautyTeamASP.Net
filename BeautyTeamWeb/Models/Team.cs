using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeautyTeamWeb.Models
{
    /// <summary>
    /// Users can join team to do team works.
    /// </summary>
    public class Team : Group
    {
        [JsonIgnore]
        public virtual List<Project> GroupProjects { get; set; }
        public override string Type => "Team";
    }
    /// <summary>
    /// Gives definition of a Project in a team. Like BeautyTeam project in Obisoft.
    /// 
    /// There is not definition for relation people of a project.
    /// </summary>
    public class Project
    {
        public virtual int ProjectId { get; set; }
        public virtual int GroupId { get; set; }
        [JsonIgnore]
        public virtual Team Group { get; set; }
        [Required]
        public virtual string ProjectName { get; set; }
        public virtual string ProjectDescription { get; set; }
        public virtual List<GroupEvent> ProjectEvents { get; set; }
        public virtual List<GroupTask> ProjectTasks { get; set; }
    }
    /// <summary>
    /// Gives definition of an Event in a team. Like a meeting.
    /// </summary>
    public class GroupEvent : Event
    {
        public virtual int ProjectId { get; set; }
        [JsonIgnore]
        public virtual Project Project { get; set; }
        public virtual List<EU_Relation> EU_Relation { get; set; }
        public override string TypeName => "GroupEvent";
    }
    /// <summary>
    /// Gives definition of a task in a team. Like fix some bug.
    /// </summary>
    public class GroupTask : Task
    {
        public virtual int ProjectId { get; set; }
        [JsonIgnore]
        public virtual Project Project { get; set; }
        public virtual List<TU_Relation> TU_Relation { get; set; }
        public override string TypeName => "GroupTask";
    }
}