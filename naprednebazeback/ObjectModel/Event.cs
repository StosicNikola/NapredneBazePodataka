using System;
using System.Collections.Generic;

namespace naprednebazeback.ObjectModel
{
    public class Event
    {
        public virtual long Id { get; set;}
        public virtual string name { get; set; }
        public virtual DateTime date { get; set; }
        public virtual int difficulty { get; set; }
        public virtual string about { get; set; }
        public virtual string type { get; set; }
        public virtual long mountainTopId {get; set;}

        public Event() {}

    }
    public class Race:Event
    {
       
    }
    public class Hike:Event
    {
        
        
    }
}