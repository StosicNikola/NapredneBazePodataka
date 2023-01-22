using System;
using naprednebazeback.ObjectModel;

namespace naprednebazeback.DTOs
{
    public class MountainRunnerView
    {
        public long PersonId {get; set;}
        public string Name {get; set;}
        public double Score {get; set;}
        public MountainTop MountainTop {get; set;}
        public DateTime TimeStampRunner {get; set;}

        public MountainRunnerView(){
            
        }

        public MountainRunnerView(Person person, double score,MountainTop mountainTop, DateTime time){
            PersonId = person.Id;
            Name  = person.name;
            Score = score;
            MountainTop = mountainTop;
            TimeStampRunner = time;
        }

        public MountainRunnerView(long id,string name, double score,MountainTop mountainTop ,DateTime time){
            PersonId = id;
            Name  = name;
            Score = score;
            MountainTop = mountainTop;
            TimeStampRunner = time;
        }

    }
}