using System.Collections.Generic;
using System;
namespace naprednebazeback.ObjectModel
{
    public class Region
    {
        public virtual long Id { get; set; }
        public virtual string name { get; set; }
        public virtual List<Mountain> Mountains { get; set;}

        public Region() {}
    }
}