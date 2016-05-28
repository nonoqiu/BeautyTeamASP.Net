using Newtonsoft.Json;

namespace BeautyTeamWeb.Models
{
    public class PersonalTask : Task
    {
        public virtual string ObisoftUserId { get; set; }
        [JsonIgnore]
        public virtual ObisoftUser ObisoftUser { get; set; }
    }
    public class PersonalEvent : Event
    {
        public virtual string ObisoftUserId { get; set; }
        [JsonIgnore]
        public virtual ObisoftUser ObisoftUser { get; set; }
    }
}