using System;
using naprednebazeback.ObjectModel;

namespace naprednebazeback.DTOs
{
    public class MountainRunnerView
    {
        public long PersonId {get; set;}
        public string Name {get; set;}
        public double Score {get; set;}
        public Mountain Mountain {get; set;}
        public DateTime TimeStampRunner {get; set;}

        public MountainRunnerView(){
            
        }

        public MountainRunnerView(Person person, double score,Mountain mountain, DateTime time){
            PersonId = person.Id;
            Name  = person.name;
            Score = score;
            Mountain = mountain;
            TimeStampRunner = time;
        }

        public MountainRunnerView(long id,string name, double score,Mountain mountain ,DateTime time){
            PersonId = id;
            Name  = name;
            Score = score;
            Mountain = mountain;
            TimeStampRunner = time;
        }

    }
}