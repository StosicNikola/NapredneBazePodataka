using System.Collections.Generic;

namespace back.ObjectModel
{
    public class Region
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual List<Mountain> Mountains { get; set;}

        public Region() {}
    }
}