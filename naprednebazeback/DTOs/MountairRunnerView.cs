using System;
using naprednebazeback.ObjectModel;

namespace naprednebazeback.DTOs
{
    public class MountairRunnerView
    {
        public Guid PersonId {get; set;}
        public string Name {get; set;}
        public int Score {get; set;}
        public DateTime TimeStampRunner {get; set;}

        public MountairRunnerView(){
            
        }

        public MountairRunnerView(Person person, int score, DateTime time){
            PersonId = person.Id;
            Name  = person.name;
            Score = score;
            TimeStampRunner = time;
        }

    }
}