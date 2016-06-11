using BeautyTeamWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.ViewModels
{
    public class ApplyJobModel
    {
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
    }

    public class UserApplyJobModel : ApplyJobModel
    {
        public virtual int UserApplyJobModelId { get; set; }

        public string ObisoftUserId { get; set; }
        public ObisoftUser ObisoftUser { get; set; }
    }
}