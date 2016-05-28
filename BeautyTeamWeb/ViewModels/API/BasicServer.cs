using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.Models
{
    public class ServerReply
    {
        public System.Net.HttpStatusCode StatusCode { get; set; }
    }
    public class ObiValue<T> : ServerReply where T : struct
    {
        public virtual T Value { get; set; }
    }
    public class ObiObject<T> : ServerReply where T : class
    {
        public virtual T Object { get; set; }
    }
    public class ObiList<T> : ServerReply//, IEnumerable<T>
    {
        public virtual IEnumerable<T> List { get; set; }

        //public IEnumerator<T> GetEnumerator() => List.GetEnumerator();
       // IEnumerator IEnumerable.GetEnumerator() => List.GetEnumerator();
    }
}