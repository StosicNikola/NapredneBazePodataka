using ServiceStack.Redis;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System;
using naprednebazeback.DTOs;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using naprednebazeback.ObjectModel;

namespace naprednebazeback.RedisDataLayer
{
    public class RedisDao
    {
         private ILogger _logger;
         readonly string MainLeaderboardForAllPerson = "MainLeaderboard";
         public Dictionary<long,string> PersonOwnScoreForAllTime ;
         readonly RedisClient redis = new RedisClient(RedisConfig.SingleHost);

         public RedisDao(ILogger logger){
            PersonOwnScoreForAllTime = UploadLeaderboardNamesFromRedis();   
            _logger = logger;       
         }

         public RedisDao(){
            
         }

        public RedisClient Redis {get{ return redis;} }
         public void AddRunnerToSet(MountainRunnerView mrv){
            string blub = ConvertToBlubForMain(mrv.PersonId,mrv.Name,mrv.Mountain,mrv.TimeStampRunner);
            redis.AddItemToSortedSet(MainLeaderboardForAllPerson,blub,mrv.Score);
            string nameOfLeaderboard ;
            if (PersonOwnScoreForAllTime.TryGetValue(mrv.PersonId, out nameOfLeaderboard)){
              string newBlub = ConvertToBlubForPeople(mrv);
              redis.AddItemToSortedSet(nameOfLeaderboard,newBlub,mrv.Score);
            }
            else{
              CreateNewLeaderboardForNewPerson(mrv);
              if (PersonOwnScoreForAllTime.TryGetValue(mrv.PersonId, out nameOfLeaderboard)){
                 string newBlub = ConvertToBlubForPeople(mrv);
                 redis.AddItemToSortedSet(nameOfLeaderboard,newBlub,mrv.Score);
              }
            }
         }

         public List<MountainRunnerView> GetMainLeaderboardFrom0toNPeople(int numOfReturnItems){
            IDictionary<string,double> items = GetItemWithScoreForMainLeaderboard(numOfReturnItems);
            List<MountainRunnerView> mrvs  = new List<MountainRunnerView>();
            foreach(KeyValuePair<string,double> item in items){
              string[] subs = item.Key.Split("#");
              long id = (long)Convert.ToDouble(subs[0]);
              Mountain mountain = new Mountain();
              mountain.Id = (long)Convert.ToDouble(subs[2]);
              mountain.name = subs[3];
              DateTime date = DateTime.ParseExact(subs[4], "dd/MM/yyyy HH:mm:ss", null);
              mrvs.Add( new MountainRunnerView(id,subs[1],item.Value,mountain, date));
            }
            
            return mrvs;
         }

         public List<string> GetAllItemsFromSortedSet(){
          return redis.GetAllItemsFromSortedSet(MainLeaderboardForAllPerson);
         }

         public string ConvertToBlubForMain(long id,string name,Mountain mountain, DateTime time){
          string blub = id + "#"+ name + "#" +mountain.Id+"#" + mountain.name+"#" + time.ToString("dd/MM/yyyy HH:mm:ss");
          return blub;
         }

         public string ConvertToBlubForPeople(MountainRunnerView mrv){
         // string blub = id + "#"+ name + "#" + time.ToString("dd/MM/yyyy HH:mm:ss");
          string blub = mrv.TimeStampRunner.ToString("dd/MM/yyyy HH:mm:ss") + "#" + mrv.Mountain.Id + "#" + mrv.Mountain.name;
          return blub;
         }


         public IDictionary<string,double> GetItemWithScoreForMainLeaderboard(int numOfReturnItems){
           return redis.GetRangeWithScoresFromSortedSet(MainLeaderboardForAllPerson,0,numOfReturnItems);
         }

         public void CreateNewLeaderboardForNewPerson(MountainRunnerView mrv){
              string Value = "Leaderboard#"+mrv.PersonId+"#"+mrv.Name;
              PersonOwnScoreForAllTime.Add(mrv.PersonId,Value);
         }

         public void DeleteAllItemInDb(){
          redis.FlushAll();
         }

          public List<MountainRunnerView> GetLeaderboardForPersonFrom0toNitems(int numOfReturnItems, long personId){
            IDictionary<string,double> items = GetItemWithScoreForPersonLeaderboard(numOfReturnItems,personId);
            string name = this.PersonOwnScoreForAllTime[personId];
            string[] subs = name.Split("#");
            List<MountainRunnerView> mrvs  = new List<MountainRunnerView>();
            foreach(KeyValuePair<string,double> item in items){
              string[] s1 = item.Key.Split("#");
              Mountain mountain = new Mountain();
              mountain.Id = (long)Convert.ToDouble(s1[1]);
              mountain.name = s1[2];
               DateTime date = DateTime.ParseExact(s1[0], "dd/MM/yyyy HH:mm:ss", null);
             // mrvs.Add( new MountainRunnerView(personId,subs[2],item.Value,mountain, Convert.ToDateTime(item.Key)));
             mrvs.Add( new MountainRunnerView(personId,subs[2],item.Value,mountain, date));
            }   
            return mrvs;
         }

           
           public IDictionary<string,double> GetItemWithScoreForPersonLeaderboard(int numOfReturnItems , long PersonId){
            string nameOfLeaderboard;
            if (PersonOwnScoreForAllTime.TryGetValue(PersonId, out nameOfLeaderboard)){
              return redis.GetRangeWithScoresFromSortedSet(nameOfLeaderboard,0,numOfReturnItems);
            }
            return new Dictionary<string,double>();
         }      

         public Dictionary<long,string> GetDictionary(){
          return this.PersonOwnScoreForAllTime;
         }

         public Dictionary<long,string> UploadLeaderboardNamesFromRedis(){
            List<string> leaderboards = redis.GetAllKeys();
            leaderboards.Remove(MainLeaderboardForAllPerson);
             Dictionary<long,string> items = new Dictionary<long,string>();
            foreach(string item in leaderboards){
              string[] subs = item.Split("#");
              long id = (long)Convert.ToDouble(subs[1]);
              items.Add(id,item);
            }           
            return items;
            
         }

     

        public void SaveMessagesToHash(MessageView message){
            string keyHash = GetKeyForHash(message.Room);
            string hashColumn = GetColumnForHash(message.TimeSendMessage);
            string msg = GetValueForHash(message);
            string msgFromHash = redis.GetValueFromHash(keyHash,hashColumn);
            redis.SetEntryInHash(keyHash,hashColumn,msgFromHash+"$"+msg);
        }

        public string GetKeyForHash(string room){
          return "MessageRoom#"+ room;
        }

        public string GetColumnForHash(DateTime time){
          return "MessagesForDay#"+ time.Year+"#"+time.DayOfYear;
        }
        public string GetValueForHash(MessageView message){
          return message.Name + "#"+message.PersonId+ "#" + message.Message + "#" + message.TimeSendMessage.ToString("HH:mm:ss");
        }

        public List<MessageView> GetMessageRoomForToday(String room){
            string keyHash = GetKeyForHash(room);
            string hashColumn  = GetColumnForHash(DateTime.Now);
            string blub = redis.GetValueFromHash(keyHash,hashColumn);
            if(blub == null) return new List<MessageView>();
            string[] msgs = blub.Split("$");
            List<MessageView> msg = new List<MessageView>();      
             foreach (string s in msgs.Skip(1)){
               string[] m = s.Split("#");
               long id = (long)Convert.ToDouble(m[1]);
               if(id<0){
                  msg.Add(new MessageView(id,m[0],room,m[2],Convert.ToDateTime(m[3])));
               }
               else{
                  msg.Add(new MessageView(id,m[0],room,m[2],Convert.ToDateTime(m[3])));
               }
            }

            return msg;
        }

        
    }
}