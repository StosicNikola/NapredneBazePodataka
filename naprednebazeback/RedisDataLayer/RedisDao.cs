using ServiceStack.Redis;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System;

namespace naprednebazeback.RedisDataLayer
{
    public class RedisDao
    {
         readonly string RedisLeaderboard = "Leaderboard";
         readonly RedisClient redis = new RedisClient(RedisConfig.SingleHost);

         public RedisDao(){
           // redis.PushItemToList("myKey","Nikola");
         }

         public void AddRunnerToSet(String name,DateTime time,long score){
            string blub = ConvertToBlub(name,time);
            redis.AddItemToSortedSet(RedisLeaderboard,blub,score);
            
         }

         public string GetFirstLeader(){
            List<string> record = GetAllItemsFromSortedSet();
            string[] subs = record.FirstOrDefault().Split("#");

            return record.FirstOrDefault();
         }

         public List<string> GetAllItemsFromSortedSet(){
          return redis.GetAllItemsFromSortedSet(RedisLeaderboard);
         }

         public string ConvertToBlub(string name, DateTime time){
          string blub = name + "#" + time.ToString("dd/MM/yyyy HH:mm:ss");
          return blub;
         }
    }
}