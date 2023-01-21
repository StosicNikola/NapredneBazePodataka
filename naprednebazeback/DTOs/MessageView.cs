using System;
using naprednebazeback.ObjectModel;

namespace naprednebazeback.DTOs
{
    public class MessageView
    {
        public long PersonId {get; set;}
        public string Name {get; set;}
        public string Room {get; set;}
        public string Message {get; set;}
        public DateTime TimeSendMessage {get; set;}

        public MessageView(){
            
        }

        public MessageView(Person person,string room, string msg, DateTime time){
            PersonId = person.Id;
            Name  = person.name;
            Room = room;
            Message = msg;
            TimeSendMessage = time;
        }

        public MessageView(long id,string name,string room, string msg, DateTime time){
            PersonId = id;
            Name  = name;
            Room = room;
            Message = msg;
            TimeSendMessage = time;
        }

    }
}