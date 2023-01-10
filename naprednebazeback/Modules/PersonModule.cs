using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using naprednebazeback.ObjectModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neo4jClient;


namespace naprednebazeback.Modules
{
    public class PersonModule
    {
        private static IGraphClient _graphClient;
        private static ILogger _logger;
        public PersonModule(IGraphClient graphClient, ILogger logger)
        {
            _logger = logger;
            _graphClient = graphClient;
        }
        public async IAsyncEnumerable<object> CreateMountaineer(string name, string surname, int age, long memberCard)
        {
            var obj = new object();
            try
            {
                Guid id = Guid.NewGuid();
                Dictionary<string, object> dictParams = new Dictionary<string, object>();
                dictParams.Add("Id", id);
                dictParams.Add("Name", name);
                dictParams.Add("Surname", surname);
                dictParams.Add("Age", age);
                dictParams.Add("MemberCard", memberCard);
                dictParams.Add("NumberOfClimbs", 0);
                obj = await _graphClient.Cypher.Create("(m:Mountaineer{Id:$Id, name:$Name, surname:$Surname, age:$Age, memberCard:$MemberCard,numberOfClimbs:$NumberOfClimbs })").WithParams(dictParams).Return(m => m.As<Mountaineer>()).ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error creating mointainer! " + e.Message);
            }
            yield return obj;
        }

        public async IAsyncEnumerable<object> CreateHikingGuide(string name, string surname, int age, long licenseNumber)
        {
            var obj = new object();
            try
            {
                Guid id = Guid.NewGuid();
                Dictionary<string, object> dictParams = new Dictionary<string, object>();
                dictParams.Add("Id", id);
                dictParams.Add("Name", name);
                dictParams.Add("Surname", surname);
                dictParams.Add("Age", age);
                dictParams.Add("LicenseNumber", licenseNumber);
                dictParams.Add("Rating", 0);
                dictParams.Add("Role", "HikingGuide");
                obj = await _graphClient.Cypher.Create("(m:HikingGuide{Id:$Id, name:$Name, surname:$Surname, age:$Age, licenseNumber:$LicenseNumber,Rating:$Rating, role:$Role })").WithParams(dictParams).Return(m => m.As<HikingGuide>()).ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error creating Hiking guide! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> CreateReferee(string name, string surname, int age)
        {
            var obj = new object();
            try
            {
                Guid id = Guid.NewGuid();
                Dictionary<string, object> dictParams = new Dictionary<string, object>();
                dictParams.Add("Id", id);
                dictParams.Add("Name", name);
                dictParams.Add("Surname", surname);
                dictParams.Add("Age", age);
                dictParams.Add("Role", "Referee");
                obj = await _graphClient.Cypher.Create("(m:Referee{Id:$Id, name:$Name, surname:$Surname, age:$Age, role:$Role })").WithParams(dictParams).Return(m => m.As<Referee>()).ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error creating Hiking guide! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnAllMountaineers()
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(m:Mountaineer)").Return(m => m.As<Mountaineer>()).ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error returning all mountaineers! " + e.Message);
            }
            yield return obj;
        }

        public async IAsyncEnumerable<object> ReturnAllReferees()
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(r:Referee)").Return(r => r.As<Referee>()).ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error returning all Referees! " + e.Message);
            }
            yield return obj;
        }

        public async IAsyncEnumerable<object> ReturnAllHikingGuids()
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(hg:HikingGuide)").Return(hg => hg.As<HikingGuide>()).ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error returning all Hiking Guides! " + e.Message);
            }
            yield return obj;
        }

        public async IAsyncEnumerable<object> ReturnRefereeByName(string name)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(r:Referee)").Where((Referee r) => r.name == name).Return(r => r.As<Referee>()).ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error returning referee! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnRefereeById(Guid id)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(r:Referee)").Where((Referee r) => r.Id == id).Return(r => r.As<Referee>()).ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error returning referee! " + e.Message);
            }
            yield return obj;
        }

        public async IAsyncEnumerable<object> ReturnMounatineerByName(string name)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(m:Mountaineer)").Where((Mountaineer m) => m.name == name).Return(m => m.As<Mountaineer>()).ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error returning Mountaineer ! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnMounatineerById(Guid id)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(m:Mountaineer)").Where((Mountaineer m) => m.Id == id).Return(m => m.As<Mountaineer>()).ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error returning Mountaineer! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnHikingGuideByName(string name)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(hg:HikingGuide)").Where((HikingGuide hg) => hg.name == name).Return(hg => hg.As<HikingGuide>()).ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error returning Hiking Guide! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnHikingGuideById(Guid id)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(hg:HikingGuide)").Where((HikingGuide hg) => hg.Id == id).Return(hg => hg.As<HikingGuide>()).ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error returning Hiking Guide! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> UpdateReferee(Referee referee)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(r:Referee)")
                                                .Where((Referee r) => r.Id == referee.Id)
                                                .Set("r:$referee")
                                                .WithParam("referee", referee)
                                                .Return(r => r.As<Referee>())
                                                .ResultsAsync;

            }
            catch (Exception e)
            {
                _logger.LogError("Error updating referee! " + e.Message);
            }
            yield return obj;
        }

        public async IAsyncEnumerable<object> UpdateMountaineer(Mountaineer mountaineer)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(m:Mountaineer)")
                                                .Where((Mountaineer m) => m.Id == mountaineer.Id)
                                                .Set("m:$Mountaineer")
                                                .WithParam("Mountaineer", mountaineer)
                                                .Return(m => m.As<Mountaineer>())
                                                .ResultsAsync;

            }
            catch (Exception e)
            {
                _logger.LogError("Error updating Mountaineer! " + e.Message);
            }
            yield return obj;
        }

         public async IAsyncEnumerable<object> UpdateHikingGuide(HikingGuide hikingGuide)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(hg:HikingGuide)")
                                                .Where((HikingGuide hg) => hg.Id == hikingGuide.Id)
                                                .Set("hg:$HikingGuide")
                                                .WithParam("HikingGuide", hikingGuide)
                                                .Return(hg => hg.As<HikingGuide>())
                                                .ResultsAsync;

            }
            catch (Exception e)
            {
                _logger.LogError("Error updating Hiking Guide! " + e.Message);
            }
            yield return obj;
        }
        public async void DeleteReferee(Guid id)
        {
            try
            {
                await _graphClient.Cypher.Match("(r:Referee)")
                                                .Where((Referee r)=>r.Id == id)
                                                .DetachDelete("r").ExecuteWithoutResultsAsync();
            }
            catch(Exception e)
            {
                _logger.LogError("Error deleting referee! " + e.Message);
            }
        }

        public async void DeleteHikingGuide(Guid id)
        {
            try
            {
                await _graphClient.Cypher.Match("(hg:HikingGuide)")
                                                .Where((HikingGuide hg)=>hg.Id == id)
                                                .DetachDelete("hg").ExecuteWithoutResultsAsync();
            }
            catch(Exception e)
            {
                _logger.LogError("Error deleting Hiking Guide! " + e.Message);
            }
        }
        public async void DeleteMountaineer(Guid id)
        {
            try
            {
                await _graphClient.Cypher.Match("(m:Mountaineer)")
                                                .Where((Mountaineer m)=>m.Id == id)
                                                .DetachDelete("m").ExecuteWithoutResultsAsync();
            }
            catch(Exception e)
            {
                _logger.LogError("Error deleting Mountaineer! " + e.Message);
            }
        }

        public async void AddHike(Guid hikeId, Guid hikeGuideId)
        {
            try
            {
                await _graphClient.Cypher.Match("(h:Hike), (hg:HikeGuide)")
                                            .Where((Hike h, HikingGuide hg)=> h.Id==hikeId && hg.Id ==hikeGuideId)
                                            .Create("(hg)-[leads]->(h)")
                                            .ExecuteWithoutResultsAsync();
            }
            catch(Exception e)
            {
                _logger.LogError("Error adding hike! " + e.Message);
            }
        }

        public async void AddRace(Guid raceId, Guid refereeId)
        {
            try
            {
                await _graphClient.Cypher.Match("(r:Race), (refe:Referee)")
                                            .Where((Race r, Referee refe)=> r.Id==raceId && refe.Id == refereeId)
                                            .Create("(refe)-[Judge]->(r)")
                                            .ExecuteWithoutResultsAsync();
            }
            catch(Exception e)
            {
                _logger.LogError("Error adding race! " + e.Message);
            }
        }

        public async void AddMountaineerCompete(Guid raceId, Guid mountaineerId)
        {
            try
            {
                await _graphClient.Cypher.Match("(r:Race), (m:Mountaineer)")
                                            .Where((Race r, Mountaineer m)=> r.Id==raceId && m.Id == mountaineerId)
                                            .Create("(m)-[CompetesIn]->(r)")
                                            .ExecuteWithoutResultsAsync();
            }
            catch(Exception e)
            {
                _logger.LogError("Error adding race! " + e.Message);
            }
        }

        public async void AddMountaineerClimbs(Guid hikeId, Guid mountaineerId)
        {
            try
            {
                await _graphClient.Cypher.Match("(h:Hike), (m:Mountaineer)")
                                            .Where((Hike h, Mountaineer m)=> h.Id==hikeId && m.Id == mountaineerId)
                                            .Create("(m)-[Climb]->(r)")
                                            .ExecuteWithoutResultsAsync();
            }
            catch(Exception e)
            {
                _logger.LogError("Error adding race! " + e.Message);
            }
        }
        



    }

}
