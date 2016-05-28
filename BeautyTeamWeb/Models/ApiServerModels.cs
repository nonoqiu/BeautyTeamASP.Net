using System.ComponentModel.DataAnnotations;

namespace BeautyTeamWeb.Models
{

    public class EventLogDb : EventLog
    {
        [Key]
        public virtual int EventLogDbId { get; set; }
        public EventLogDb() { }
        public EventLogDb(EventLog e)
        {
            Description = e.Description;
            HappenPlatform = e.HappenPlatform;
            version = e.version;
            HappenTime = e.HappenTime;
        }
    }
}