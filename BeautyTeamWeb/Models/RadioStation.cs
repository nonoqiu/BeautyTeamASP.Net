using System.Collections.Generic;

namespace BeautyTeamWeb.Models
{
    /// <summary>
    /// Users can also join radiostation to only get events and tasks.
    /// </summary>
    public class RadioStation : Group
    {
        public virtual List<RadioEvent> RadioEvents { get; set; }
        public virtual List<RadioTask> RadioTasks { get; set; }
        public override string Type => "RadioStation";
    }
    /// <summary>
    /// Radio Task describes a task comes from a radio station.
    /// </summary>
    public class RadioTask : Task
    {
        public virtual int RadioStationGroupId { get; set; }
        public virtual RadioStation RadioStation { get; set; }
        public override string TypeName => "RadioTask";
    }
    /// <summary>
    /// Radio Event describes an event comes from a radio station.
    /// </summary>
    public class RadioEvent : Event
    {
        public virtual int RadioStationGroupId { get; set; }
        public virtual RadioStation RadioStation { get; set; }
        public override string TypeName => "RadioEvent";
    }


}