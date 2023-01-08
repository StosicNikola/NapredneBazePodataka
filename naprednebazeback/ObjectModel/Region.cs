using System.Collections.Generic;
using System;
namespace back.ObjectModel
{
    public class Region
    {
        public virtual Guid Id { get; set; }
        public virtual string name { get; set; }
        public virtual List<Mountain> Mountains { get; set;}

        public Region() {}
    }
}