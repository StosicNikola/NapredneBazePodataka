using System;
using naprednebazeback.ObjectModel;

namespace naprednebazeback.DTOs
{
    public class MountainRunnerView
    {
        public Guid PersonId {get; set;}
        public string Name {get; set;}
        public double Score {get; set;}
        public DateTime TimeStampRunner {get; set;}

        public MountainRunnerView(){
            
        }

        public MountainRunnerView(Person person, double score, DateTime time){
            PersonId = person.Id;
            Name  = person.name;
            Score = score;
            TimeStampRunner = time;
        }

        public MountainRunnerView(long id,string name, double score, DateTime time){
            PersonId = id;
            Name  = name;
            Score = score;
            TimeStampRunner = time;
        }

    }
}