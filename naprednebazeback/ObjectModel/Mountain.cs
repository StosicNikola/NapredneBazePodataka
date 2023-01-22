using System.Collections.Generic;
using System;

namespace naprednebazeback.ObjectModel
{
    public class Mountain
    {
        public virtual long Id { get; set; }
        public virtual string name { get; set; }
        public virtual float surface {get; set; }
      

        public Mountain() { } 

    }
}