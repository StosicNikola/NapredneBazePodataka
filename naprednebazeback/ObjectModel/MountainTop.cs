using System;
using System.Collections.Generic;

namespace naprednebazeback.ObjectModel
{
    public class MountainTop
    {
        public virtual long Id { get; set; }
        public virtual string name { get; set;}
        public virtual int height { get; set; }

        public MountainTop() {}
    }
}