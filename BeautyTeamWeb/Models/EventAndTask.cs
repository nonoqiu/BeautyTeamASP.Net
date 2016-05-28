using System;
using System.ComponentModel.DataAnnotations;

namespace BeautyTeamWeb.Models
{
    /// <summary>
    /// Which can be shown in the notice list.
    /// </summary>
    public interface INoticeable
    {
        TimeSpan Before { get; set; }
        DateTime NoticeDate { get; }
        string NoticeName { get; }
        string NoticeContent { get; }
        string TypeName { get; }
    }
    /// <summary>
    /// This is a event, like a meeting or a date.
    /// </summary>
    public abstract class Event : INoticeable
    {
        [Key]
        public virtual int EventId { get; set; }
        public virtual string EventName { get; set; }
        public virtual string EventContent { get; set; }
        public virtual TimeSpan Before { get; set; }
        public virtual DateTime HappenTime { get; set; }
        public virtual DateTime EndTime { get; set; }
        public virtual TimeSpan CostTime => EndTime - HappenTime;
        public virtual DateTime NoticeDate => HappenTime - Before;
        public virtual string NoticeName => EventName;
        public virtual string NoticeContent => EventContent;
        public virtual string TypeName => "Event";
    }
    /// <summary>
    /// This is a task, like homework
    /// </summary>
    public abstract class Task : INoticeable
    {
        [Key]
        public virtual int TaskId { get; set; }
        /// <summary>
        /// Name of the Task, like "Go to shower"
        /// </summary>
        public virtual string TaskName { get; set; }
        /// <summary>
        /// Content to notice, like "Please go to shower."
        /// </summary>
        public virtual string TaskContent { get; set; }
        /// <summary>
        /// Before some time to notice the user.
        /// </summary>
        public virtual TimeSpan Before { get; set; }
        /// <summary>
        /// Real Time of a Task.
        /// </summary>
        public virtual DateTime DeadLine { get; set; }
        /// <summary>
        /// Notice time. Means real time - notice before
        /// </summary>
        public virtual DateTime NoticeDate => DeadLine - Before;
        public virtual string NoticeName => TaskName;
        public virtual string NoticeContent => TaskContent;
        public virtual string TypeName => "Task";
    }
}