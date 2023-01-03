using System.Collections.Generic;

namespace back.ObjectModel
{
    public class Mountain
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual float Surface {get; set; }
        public virtual MountainTop HighestPoint { get; set;}
        public virtual List<MountainTop> MountainTops { get; set;}
        public virtual Region Region { get; set;}

        public Mountain() { } 

    }
}