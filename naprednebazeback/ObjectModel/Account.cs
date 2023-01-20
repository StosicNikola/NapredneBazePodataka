using System;
using System.Collections.Generic;

namespace naprednebazeback.ObjectModel
{
    public class Account
    {
        public virtual long Id { get; set; }
        public virtual string email { get; set; }
        public virtual string password { get; set; }
    }
}
