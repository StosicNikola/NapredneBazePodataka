namespace back.ObjectModel
{
    public class Person
    {
        public virtual long Id { get; protected set; }
        public virtual string Name { get; set;}
        public virtual string Surname {get; set;}     
        public virtual int Age {get; set;}

        public Person(){ }
    }

    public class Mountaineer : Person {
         public virtual long MemberCard {get; set;}
         public virtual int NumberOfClimbs {get; set;}
    }

    public class HikingGuide : Person {
        public virtual long LicenseNumber { get; set;}
        public virtual int Rating { get; set; }
    }

    public class Referee : Person {
        
    }
}