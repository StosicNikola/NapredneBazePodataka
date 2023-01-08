using System;
using System.Collections.Generic;

namespace naprednebazeback.ObjectModel
{
    public class MountainTop
    {
        public virtual Guid Id { get; set; }
        public virtual string name { get; set;}
        public virtual int height { get; set; }
        public virtual Mountain mountainName { get; set; }
        public virtual List<Event> events { get; set; }  
        public MountainTop() {}
    }
}