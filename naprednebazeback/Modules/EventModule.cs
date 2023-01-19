using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using naprednebazeback.ObjectModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neo4jClient;

namespace naprednebazeback.Modules
{
    public class EventModule
    {   
        private IGraphClient _graphClient;
        private ILogger _logger;

        public EventModule(IGraphClient graphClient, ILogger logger)
        {   
            _graphClient = graphClient;
            _logger = logger;
        }

        public async IAsyncEnumerable<object> CreateHikeEvent(string name, DateTime date, int difficulty, string about)
        {
            var obj = new object();
            try
            {
                Dictionary<string,object> dictParam = new Dictionary<string, object>();
                dictParam.Add("Name", name);
                dictParam.Add("Date", date);
                dictParam.Add("Difficulty", difficulty);
                dictParam.Add("About", about);
                obj = await _graphClient.Cypher.Create("(h:HikeEvent{ name:$Name, date:$Date, difficulty:$Difficulty, about:$About})")
                                                .WithParams(dictParam)
                                                .With("h{.*, Id:id(h)} AS hiking")
                                                .Return(hiking=>hiking.As<Hike>())
                                                .ResultsAsync;
            }
            catch(Exception e)
            {
                _logger.LogError("Error creating hike event! " + e.Message);
            }
            yield return obj;
        }
         public async IAsyncEnumerable<object> CreateRaceEvent(string name, DateTime date, int difficulty,string about)
        {
            var obj = new object();
            try
            {
                Dictionary<string,object> dictParam = new Dictionary<string, object>();
                dictParam.Add("Name", name);
                dictParam.Add("Date", date);
                dictParam.Add("Difficulty", difficulty);
                 dictParam.Add("About", about);
                obj = await _graphClient.Cypher.Create("(r:RaceEvent{ name:$Name, date:$Date, difficulty:$Difficulty,about:$About})")
                                                .WithParams(dictParam)
                                                .With("r{.*, Id:id(r)} AS race")
                                                .Return(race=>race.As<Race>())
                                                .ResultsAsync;
            }
            catch(Exception e)
            {
                _logger.LogError("Error creating hike event! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnAllHikeEvents()
        {
            var obj = await _graphClient.Cypher.Match("(h:HikeEvent)")
                                                .With("h{.*, Id:id(h)} AS hiking")
                                                .Return(hiking=>hiking.As<Hike>())
                                                .ResultsAsync;                    
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnHikeEventById(long id)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(h:HikeEvent)")
                                                .Where("id(h)=$Id")
                                                .WithParam("Id",id)
                                                .With("h{.*, Id:id(h)} AS hiking")
                                                .Return(hiking=>hiking.As<Hike>())
                                                .ResultsAsync;                                               
            }
            catch(Exception e)
            {
                _logger.LogError("Error returning hike! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnHikeEventByName(string name)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(h:HikeEvent)")
                                                .Where((Hike h)=> h.name == name)
                                                .With("h{.*, Id:id(h)} AS hiking")
                                                .Return(hiking=>hiking.As<Hike>())
                                                .ResultsAsync;                                          
            }
            catch(Exception e)
            {
                _logger.LogError("Error returning hike! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnAllRaceEvents()
        {
            var obj = await _graphClient.Cypher.Match("(r:RaceEvent)")
                                                .With("r{.*, Id:id(r)} AS race")
                                                .Return(race=>race.As<Race>())
                                                .ResultsAsync;    
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnRaceEventById(long id)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(r:RaceEvent)")
                                                .Where("id(r)=$Id")
                                                .WithParam("Id",id)
                                                .With("r{.*, Id:id(r)} AS race")
                                                .Return(race=>race.As<Race>())
                                                .ResultsAsync;                                            
            }
            catch(Exception e)
            {
                _logger.LogError("Error returning race! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnRaceEventByName(string name)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(r:RaceEvent)")
                                                .Where((Race r)=> r.name == name)
                                                .With("r{.*, Id:id(r)} AS race")
                                                .Return(race=>race.As<Race>())
                                                .ResultsAsync;                                        
            }
            catch(Exception e)
            {
                _logger.LogError("Error returning race! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> UpdateRace(Race race)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(r:RaceEvant)")
                                                .Where("id(r)=$Id")
                                                .WithParam("Id", race.Id)
                                                .Set("r=$race")
                                                .WithParam("race",new {
                                                    race.name,
                                                    race.date,
                                                    race.difficulty
                                                })
                                                .With("r{.*, Id:id(r)} AS race")
                                                .Return(race=>race.As<Race>())
                                                .ResultsAsync; 
            }
            catch(Exception e)
            {
                _logger.LogError("Error updating race! " + e.Message);
            }
            yield return obj;
        }
         public async IAsyncEnumerable<object> UpdateHike(Hike hike)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(h:HikeEvant)")
                                                .Where("id(h)=$Id")
                                                .WithParam("Id",hike.Id)
                                                .Set("h=$hike")
                                                .WithParam("hike",new {
                                                    hike.name,
                                                    hike.date,
                                                    hike.difficulty
                                                })
                                                .With("h{.*, Id:id(h)} AS hiking")
                                                .Return(hiking=>hiking.As<Hike>())
                                                .ResultsAsync;    
            }
            catch(Exception e)
            {
                _logger.LogError("Error updating hike! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> DeleteRace(long id)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("r:RaceEvent")
                                                .Where("id(h)=$Id")
                                                .WithParam("Id",id)
                                                .DetachDelete("r")
                                                .With("r{.*, Id:id(r)} AS race")
                                                .Return(race=>race.As<Race>())
                                                .ResultsAsync; 
            }   
            catch(Exception e)
            {
                _logger.LogError("Error deleting race! " + e.Message);
            }
            yield return obj;
        }
          public async IAsyncEnumerable<object> DeleteHike(long id)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("h:HikeEvent")
                                                .Where("id(h)=$Id")
                                                .WithParam("Id",id)
                                                .DetachDelete("h")
                                                .With("h{.*, Id:id(h)} AS hiking")
                                                .Return(hiking=>hiking.As<Hike>())
                                                .ResultsAsync;
            }   
            catch(Exception e)
            {
                _logger.LogError("Error deleting race! " + e.Message);
            }
            yield return obj;
        }

        public async IAsyncEnumerable<object> AddMountainTopForRace(long mountainTopId, long raceId)
        {
            var obj = new object();
            try
            {
                Dictionary<string, object> dictParam = new Dictionary<string, object>();
                dictParam.Add("mountainTopId",mountainTopId);
                dictParam.Add("raceId",raceId);
                obj = await _graphClient.Cypher.Match("(m:MountainTop), (r: RaceEvent)")
                                                .Where("id(m)=$mountainTopId and id(r)=$raceId")
                                                .WithParams(dictParam)
                                                .Create("(r)-[isHeldOn]->(m)")
                                                .With("r{.*, Id:id(r)} AS race")
                                                .Return(race=>race.As<Race>())
                                                .ResultsAsync; 
            }
            catch(Exception e)
            {
                _logger.LogError("Error adding mountain top for race! " + e.Message);
            }
            yield return obj;
        }
         public async IAsyncEnumerable<object> AddMountainTopForHike(long mountainTopId, long hikeId)
        {
            var obj = new object();
            try
            {
                Dictionary<string, object> dictParam = new Dictionary<string, object>();
                dictParam.Add("mountainTopId",mountainTopId);
                dictParam.Add("hikeId",hikeId);
                obj = await _graphClient.Cypher.Match("(m:MountainTop), (h: HikeEvent)")
                                                .Where("id(m)=$mountainTopId and id(h)=$hikeId")
                                                .WithParams(dictParam)
                                                .Create("(h)-[isHikingOn]->(m)")
                                                .With("h{.*, Id:id(h)} AS hiking")
                                                .Return(hiking=>hiking.As<Hike>())
                                                .ResultsAsync;
            }
            catch(Exception e)
            {
                _logger.LogError("Error adding mountain top for race! " + e.Message);
            }
            yield return obj;
        }

      

    }

}
