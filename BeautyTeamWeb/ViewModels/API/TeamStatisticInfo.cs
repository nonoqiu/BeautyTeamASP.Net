using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.ViewModels
{
    public class TeamStatisticInfo
    {
        public int Joined { get; set; } 
        public int Owned { get; set; }
        public int Admined { get; set; } 
        public int Teams { get; set; } 
        public int InfoStations { get; set; } 
        public int PeopleYouLeading { get; set; }
        public int TaskFinished { get; set; }
    }
}