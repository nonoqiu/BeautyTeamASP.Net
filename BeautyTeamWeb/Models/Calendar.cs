using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.Models
{
    public class Calendar
    {
        public List<Event> Events { get; set; }
        public List<Task> Tasks { get; set; }
        public IEnumerable<Event> CurrentCalendar=> Events.Where(t => t.NoticeDate > DateTime.Now);
        public IEnumerable<EConflict> CurrentEConflict
        {
            get
            {
#warning NotImplementedException
                throw new NotImplementedException();
            }
        }
    }
    public class EConflict
    {
        public Event LEvent { get; set; }
        public Task LTask { get; set; }
    }
}