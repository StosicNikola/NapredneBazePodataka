using System.Collections.Generic;
using System;

namespace naprednebazeback.ObjectModel
{
    public class Mountain
    {
        public virtual Guid Id { get; set; }
        public virtual string name { get; set; }
        public virtual float surface {get; set; }
        public virtual MountainTop highestPoint { get; set;}
        public virtual List<MountainTop> mountainTops { get; set;}
        public virtual Region region { get; set;}

        public Mountain() { } 

    }
}