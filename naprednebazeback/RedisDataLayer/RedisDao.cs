using ServiceStack.Redis;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System;
using naprednebazeback.DTOs;
using Microsoft.Extensions.Logging;

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

         public void AddRunnerToSet(MountainRunnerView mrv){
            string blub = ConvertToBlubForMain(mrv.PersonId,mrv.Name,mrv.TimeStampRunner);
            redis.AddItemToSortedSet(MainLeaderboardForAllPerson,blub,mrv.Score);
            string nameOfLeaderboard ;
            if (PersonOwnScoreForAllTime.TryGetValue(mrv.PersonId, out nameOfLeaderboard)){
              string newBlub = ConvertToBlubForPeople(mrv.TimeStampRunner);
              redis.AddItemToSortedSet(nameOfLeaderboard,newBlub,mrv.Score);
            }
            else{
              CreateNewLeaderboardForNewPerson(mrv);
              if (PersonOwnScoreForAllTime.TryGetValue(mrv.PersonId, out nameOfLeaderboard)){
                 string newBlub = ConvertToBlubForPeople(mrv.TimeStampRunner);
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
              mrvs.Add( new MountainRunnerView(id,subs[1],item.Value, Convert.ToDateTime(subs[2])));
            }
            
            return mrvs;
         }

         public List<string> GetAllItemsFromSortedSet(){
          return redis.GetAllItemsFromSortedSet(MainLeaderboardForAllPerson);
         }

         public string ConvertToBlubForMain(long id,string name, DateTime time){
          string blub = id + "#"+ name + "#" + time.ToString("dd/MM/yyyy HH:mm:ss");
          return blub;
         }

         public string ConvertToBlubForPeople(DateTime time){
         // string blub = id + "#"+ name + "#" + time.ToString("dd/MM/yyyy HH:mm:ss");
          string blub = time.ToString("dd/MM/yyyy HH:mm:ss");
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
              mrvs.Add( new MountainRunnerView(personId,subs[2],item.Value, Convert.ToDateTime(item.Key)));
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
    }
}