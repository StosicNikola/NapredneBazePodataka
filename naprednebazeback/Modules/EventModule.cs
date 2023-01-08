using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using back.ObjectModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neo4jClient;

namespace back.Modules
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

        public async IAsyncEnumerable<object> CreateHikeEvent(string name, DateTime date, int difficulty)
        {
            var obj = new object();
            try
            {
                Dictionary<string,object> dictParam = new Dictionary<string, object>();
                dictParam.Add("Id", Guid.NewGuid());
                dictParam.Add("Name", name);
                dictParam.Add("Date", date);
                dictParam.Add("Difficulty", difficulty);
                obj = await _graphClient.Cypher.Create("(h:HikeEvent{Id:$Id, name:$Name, date:$Date, difficulty:$Difficulty})")
                                                .WithParams(dictParam)
                                                .Return(h=>h.As<Hike>())
                                                .ResultsAsync;
            }
            catch(Exception e)
            {
                _logger.LogError("Error creating hike event! " + e.Message);
            }
            yield return obj;
        }
         public async IAsyncEnumerable<object> CreateRaceEvent(string name, DateTime date, int difficulty)
        {
            var obj = new object();
            try
            {
                Dictionary<string,object> dictParam = new Dictionary<string, object>();
                dictParam.Add("Id", Guid.NewGuid());
                dictParam.Add("Name", name);
                dictParam.Add("Date", date);
                dictParam.Add("Difficulty", difficulty);
                obj = await _graphClient.Cypher.Create("(r:RaceEvent{Id:$Id, name:$Name, date:$Date, difficulty:$Difficulty})")
                                                .WithParams(dictParam)
                                                .Return(r=>r.As<Race>())
                                                .ResultsAsync;
            }
            catch(Exception e)
            {
                _logger.LogError("Error creating hike event! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnHikeEventById(Guid id)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(h:HikeEvent)")
                                                .Where((Hike h)=> h.Id == id)
                                                .Return(h=>h.As<Hike>())
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
                                                .Return(h=>h.As<Hike>())
                                                .ResultsAsync;                                           
            }
            catch(Exception e)
            {
                _logger.LogError("Error returning hike! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnRaceEventById(Guid id)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(r:RaceEvent)")
                                                .Where((Race r)=> r.Id == id)
                                                .Return(r=>r.As<Race>())
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
                                                .Return(r=>r.As<Race>())
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
                                                .Where((Race r)=> r.Id == race.Id)
                                                .Set("r=$race")
                                                .WithParam("race",race)
                                                .Return(r=>r.As<Race>())
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
                                                .Where((Hike h)=> h.Id == hike.Id)
                                                .Set("r=$race")
                                                .WithParam("race",hike)
                                                .Return(h=>h.As<Hike>())
                                                .ResultsAsync; 
            }
            catch(Exception e)
            {
                _logger.LogError("Error updating hike! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> DeleteRace(Guid id)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("r:RaceEvent")
                                                .Where((Race r)=> r.Id == id)
                                                .DetachDelete("r")
                                                .Return(r=>r.As<Race>())
                                                .ResultsAsync; 
            }   
            catch(Exception e)
            {
                _logger.LogError("Error deleting race! " + e.Message);
            }
            yield return obj;
        }
          public async IAsyncEnumerable<object> DeleteHike(Guid id)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("h:HikeEvent")
                                                .Where((Hike h)=> h.Id == id)
                                                .DetachDelete("h")
                                                .Return(h=>h.As<Hike>())
                                                .ResultsAsync; 
            }   
            catch(Exception e)
            {
                _logger.LogError("Error deleting race! " + e.Message);
            }
            yield return obj;
        }

        public async IAsyncEnumerable<object> AddMountainTopForRace(Guid mountainTopId, Guid raceId)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(m:MountainTop), (r: RaceEvent)")
                                                .Where((MountainTop m, Race r)=> m.Id == mountainTopId && r.Id == raceId)
                                                .Create("(r)-[isHeldOn]->(m)")
                                                .Return(r=>r.As<Race>())
                                                .ResultsAsync;
            }
            catch(Exception e)
            {
                _logger.LogError("Error adding mountain top for race! " + e.Message);
            }
            yield return obj;
        }
         public async IAsyncEnumerable<object> AddMountainTopForHike(Guid mountainTopId, Guid hikeId)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(m:MountainTop), (h: HikeEvent)")
                                                .Where((MountainTop m, Hike h)=> m.Id == mountainTopId && h.Id == hikeId)
                                                .Create("(h)-[isHeldOn]->(m)")
                                                .Return(h=>h.As<Hike>())
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
