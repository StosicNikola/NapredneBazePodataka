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
        public virtual List<Mountaineer> participants { get; set; }

        public Event() {}

    }
    public class Race:Event
    {
        public virtual Referee referee { get; set; }
    }
    public class Hike:Event
    {
        public virtual HikingGuide guide { get; set;}
        
    }
}