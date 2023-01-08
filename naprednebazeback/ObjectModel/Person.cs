namespace naprednebazeback.ObjectModel
{
    public class Person
    {
        public virtual long Id { get; protected set; }
        public virtual string name { get; set;}
        public virtual string surname {get; set;}     
        public virtual int sge {get; set;}

        public Person(){ }
    }

    public class Mountaineer : Person {
         public virtual long memberCard {get; set;}
         public virtual int numberOfClimbs {get; set;}
    }

    public class HikingGuide : Person {
        public virtual long licenseNumber { get; set;}
        public virtual int rating { get; set; }
    }

    public class Referee : Person {
        
    }
}