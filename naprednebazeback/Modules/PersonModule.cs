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
         public async IAsyncEnumerable<object>  CreateAdmin(string name, string surname,int age,long accountId)
         {
            var obj = new object();
            try
            {
                Dictionary<string, object> dictParams = new Dictionary<string, object>();
                dictParams.Add("Name", name);
                dictParams.Add("Surname", surname);
                dictParams.Add("Age",age);
                obj = await _graphClient.Cypher.Match("(a:Account)")
                                                .Where("id(a) =$aId")
                                                .WithParam("aId", accountId)
                                                .Create("(m:Person{name:$Name, surname:$Surname, age:$Age})")
                                                .WithParams(dictParams)
                                                .Create("(m)-[h:hasAccount{role:$Role}]->(a)")
                                                .WithParam("Role","Admin")
                                                .With("m{.*, Id:id(m)} AS person")
                                                 .Return((person) => 
                                                        new {
                                                            id = person.As<Person>().Id,
                                                    
                                                            })
                                                .ResultsAsync;



            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
            }
            yield return obj;

         }
        
        public async IAsyncEnumerable<object>  CreateMountaineer(string name, string surname, int age, long memberCard,long accountId)
        {
            var obj = new object();
            try
            {
                Dictionary<string, object> dictParams = new Dictionary<string, object>();
                dictParams.Add("Name", name);
                dictParams.Add("Surname", surname);
                dictParams.Add("Age", age);
                dictParams.Add("MemberCard", memberCard);
                dictParams.Add("NumberOfClimbs", 0);
                obj = await _graphClient.Cypher.Match("(a:Account)")
                                                .Where("id(a) =$aId")
                                                .WithParam("aId", accountId)
                                                .Create("(m:Person:Mountaineer{name:$Name, surname:$Surname, age:$Age, memberCard:$MemberCard,numberOfClimbs:$NumberOfClimbs })")
                                                .WithParams(dictParams)
                                                .Create("(m)-[h:hasAccount{role:$Role}]->(a)")
                                                .WithParam("Role","Mountaineer")
                                                .With("m{.*, Id:id(m)} AS mountaineer")
                                                .Return((mountaineer) => 
                                                        new {
                                                            id = mountaineer.As<Mountaineer>().Id,
                                                    
                                                            })
                                                .ResultsAsync;
                            
            }
            catch (Exception e)
            {
                _logger.LogError("Error creating mointainer! " + e.Message);
            }
            yield return obj;
        }

        public async IAsyncEnumerable<object> CreateHikingGuide(string name, string surname, int age, long licenseNumber, long accountId)
        {
            var obj = new object();
            try
            {

                Dictionary<string, object> dictParams = new Dictionary<string, object>();
                dictParams.Add("Name", name);
                dictParams.Add("Surname", surname);
                dictParams.Add("Age", age);
                dictParams.Add("LicenseNumber", licenseNumber);
                dictParams.Add("Rating", 0);
                dictParams.Add("Role", "HikingGuide");
                obj = await _graphClient.Cypher.Match("(a:Account)")
                                                .Where("id(a) =$aId")
                                                .WithParam("aId", accountId)
                                                .Create("(h:Person:HikingGuide{ name:$Name, surname:$Surname, age:$Age, licenseNumber:$LicenseNumber,Rating:$Rating, role:$Role })")
                                                .WithParams(dictParams)
                                                .Create("(h)-[k:hasAccount{role:$Role}]->(a)")
                                                .With("h{.*, Id:id(h)} AS hiking")
                                                .Return((hiking) => hiking.As<HikingGuide>().Id)
                                                .ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error creating Hiking guide! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> CreateReferee(string name, string surname, int age, long accountId)
        {
            var obj = new object();
            try
            {
                Dictionary<string, object> dictParams = new Dictionary<string, object>();
                dictParams.Add("Name", name);
                dictParams.Add("Surname", surname);
                dictParams.Add("Age", age);
                dictParams.Add("Role", "Referee");
                obj = await _graphClient.Cypher.Match("(a:Account)")
                                                .Where("id(a) =$aId")
                                                .WithParam("aId", accountId)
                                                .Create("(r:Person:Referee{ name:$Name, surname:$Surname, age:$Age, role:$Role })")
                                                .WithParams(dictParams)
                                                .Create("(r)-[h:hasAccount{role:$Role}]->(a)")
                                                .With("r{.*, Id:id(r)} AS referee")
                                                .Return(referee => referee.As<Referee>().Id)
                                                .ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error creating Hiking guide! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnPerson(long personId)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(p:Person)")
                                                .Where("id(p)=$id")
                                                .WithParam("id",personId)
                                                .With("p{.*, Id:id(p)} AS person")
                                                .Return(person=>new{
                                                    person = person.As<Person>()
                                                })
                                                .ResultsAsync;
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnAllMountaineers()
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(m:Mountaineer)")
                                                .With("m{.*, Id:id(m)} AS mountaineer")
                                                .Return(mountaineer => mountaineer.As<Mountaineer>())
                                                .ResultsAsync;
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
                obj = await _graphClient.Cypher.Match("(r:Referee)")
                                                .With("r{.*, Id:id(r)} AS referee")
                                                .Return(referee => referee.As<Referee>())
                                                .ResultsAsync;
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
                obj = await _graphClient.Cypher.Match("(h:HikingGuide)")
                                                .With("h{.*, Id:id(h)} AS hiking")
                                                .Return(hiking => hiking.As<HikingGuide>())
                                                .ResultsAsync;
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
                obj = await _graphClient.Cypher.Match("(r:Referee)")
                                                .Where((Referee r) => r.name == name)
                                                .With("r{.*, Id:id(r)} AS referee")
                                                .Return(referee => referee.As<Referee>())
                                                .ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error returning referee! " + e.Message);
            }
            yield return obj;
        }
       public async IAsyncEnumerable<object> ReturnRefereeById(long id)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(r:Referee)")
                                                .Where("id(r)=$Id")
                                                .WithParam("Id",id)
                                                .With("r{.*, Id:id(r)} AS referee")
                                                .Return(referee => referee.As<Referee>())
                                                .ResultsAsync;
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
                obj = await _graphClient.Cypher.Match("(m:Mountaineer)")
                                                .Where((Mountaineer m) => m.name == name)
                                                .With("m{.*, Id:id(m)} AS mountaineer")
                                                .Return(mountaineer => mountaineer.As<Mountaineer>())
                                                .ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error returning Mountaineer ! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnMounatineerById(long id)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(m:Person:Mountaineer)")
                                                .Where("id(m)=$Id")
                                                .WithParam("Id",id)
                                                .With("m{.*, Id:id(m)} AS mountaineer")
                                                .Return((mountaineer) => new {
                                                    person = mountaineer.As<Mountaineer>()
                                                })
                                                .ResultsAsync;
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
                obj = await _graphClient.Cypher.Match("(h:HikingGuide)")
                                                .Where((HikingGuide hg) => hg.name == name)
                                                .With("h{.*, Id:id(h)} AS hiking")
                                                .Return(hiking => hiking.As<HikingGuide>())
                                                .ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error returning Hiking Guide! " + e.Message);
            }
            yield return obj;
        }
         
        public async IAsyncEnumerable<object> ReturnHikingGuideById(long id)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(h:HikingGuide)")
                                                .Where("id(h)=$Id")
                                                .WithParam("Id",id)
                                                .With("h{.*, Id:id(h)} AS hiking")
                                                .Return(hiking => hiking.As<HikingGuide>())
                                                .ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error returning Hiking Guide! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> UpdateReferee(Person referee)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(r:Referee)")
                                                .Where("id(r)=$Id")
                                                .WithParam("Id",referee.Id)
                                                .Set("r= $referee")
                                                .WithParam("referee", new 
                                                {
                                                    referee.name,
                                                    referee.surname,
                                                    referee.age,
                                                    referee.role
                                                })
                                                .With("r{.*, Id:id(r)} AS referee")
                                                .Return(referee => referee.As<Referee>())
                                                .ResultsAsync;

            }
            catch (Exception e)
            {
                _logger.LogError("Error updating referee! " + e.Message);
            }
            yield return obj;
        }

        public async IAsyncEnumerable<object> UpdateMountaineer(Person mountaineer)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(m:Mountaineer)")
                                                .Where("id(m)=$Id")
                                                .WithParam("Id",mountaineer.Id)
                                                .Set("m = $Mountaineer")
                                                .WithParam("Mountaineer", mountaineer)
                                                 .With("m{.*, Id:id(m)} AS mountaineer")
                                                .Return(mountaineer => mountaineer.As<Mountaineer>())
                                                .ResultsAsync;

            }
            catch (Exception e)
            {
                _logger.LogError("Error updating Mountaineer! " + e.Message);
            }
            yield return obj;
        }

         public async IAsyncEnumerable<object> UpdateHikingGuide(Person hikingGuide)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(h:HikingGuide)")
                                                .Where("id(h)=$Id")
                                                .WithParam("Id",hikingGuide.Id)
                                                .Set("h = $HikingGuide")
                                                .WithParam("HikingGuide", hikingGuide)
                                                .With("h{.*, Id:id(h)} AS hiking")
                                                .Return(hiking => hiking.As<HikingGuide>())
                                                .ResultsAsync;

            }
            catch (Exception e)
            {
                _logger.LogError("Error updating Hiking Guide! " + e.Message);
            }
            yield return obj;
        }
        public async void DeleteReferee(long id)
        {
            try
            {
                await _graphClient.Cypher.Match("(r:Referee)")
                                                .Where("id(r)=$Id")
                                                .WithParam("Id",id)
                                                .DetachDelete("r").ExecuteWithoutResultsAsync();
            }
            catch(Exception e)
            {
                _logger.LogError("Error deleting referee! " + e.Message);
            }
        }

        public async void DeleteHikingGuide(long id)
        {
            try
            {
                await _graphClient.Cypher.Match("(hg:HikingGuide)")
                                                .Where("id(h)=$Id")
                                                .WithParam("Id",id)
                                                .DetachDelete("hg").ExecuteWithoutResultsAsync();
            }
            catch(Exception e)
            {
                _logger.LogError("Error deleting Hiking Guide! " + e.Message);
            }
        }
        public async void DeleteMountaineer(long id)
        {
            try
            {
                await _graphClient.Cypher.Match("(m:Mountaineer)")
                                                .Where("id(m)=$Id")
                                                .WithParam("Id",id)
                                                .DetachDelete("m").ExecuteWithoutResultsAsync();
            }
            catch(Exception e)
            {
                _logger.LogError("Error deleting Mountaineer! " + e.Message);
            }
        }

        public async void AddHike(long hikeId, long hikeGuideId)
        {
            try
            {
                Dictionary<string,object> dictParam = new Dictionary<string, object>();
                dictParam.Add("hikeId",hikeId);
                dictParam.Add("hikeGuideId",hikeGuideId);
                await _graphClient.Cypher.Match("(h:Hike), (hg:HikeGuide)")
                                            .Where("id(h)=$hikeId and id(hg)=$hikeGuideId")
                                            .WithParams(dictParam)
                                            .Create("(hg)-[leads]->(h)")
                                            .ExecuteWithoutResultsAsync();
            }
            catch(Exception e)
            {
                _logger.LogError("Error adding hike! " + e.Message);
            }
        }

        public async void AddRace(long raceId, long refereeId)
        {
            try
            {
                Dictionary<string,object> dictParam = new Dictionary<string, object>();
                dictParam.Add("raceId",raceId);
                dictParam.Add("refereeId",refereeId);
                await _graphClient.Cypher.Match("(r:Race), (refe:Referee)")
                                            .Where("id(r)=$raceId and id(refe)=$refereeId")
                                            .WithParams(dictParam)
                                            .Create("(refe)-[Judge]->(r)")
                                            .ExecuteWithoutResultsAsync();
            }
            catch(Exception e)
            {
                _logger.LogError("Error adding race! " + e.Message);
            }
        }

        public async void AddMountaineerCompete(long raceId, long mountaineerId)
        {
            try
            {
                Dictionary<string,object> dictParam = new Dictionary<string, object>();
                dictParam.Add("raceId",raceId);
                dictParam.Add("mountaineerId",mountaineerId);
                await _graphClient.Cypher.Match("(r:Race), (m:Mountaineer)")
                                            .Where("id(r)=$raceId and id(m)=$mountaineerId")
                                            .WithParams(dictParam)
                                            .Create("(m)-[CompetesIn]->(r)")
                                            .ExecuteWithoutResultsAsync();
            }
            catch(Exception e)
            {
                _logger.LogError("Error adding race! " + e.Message);
            }
        }

        public async void AddMountaineerClimbs(long hikeId, long mountaineerId)
        {
            try
            {
                Dictionary<string,object> dictParam = new Dictionary<string, object>();
                dictParam.Add("hikeId",hikeId);
                dictParam.Add("mountaineerId",mountaineerId);
                await _graphClient.Cypher.Match("(h:Hike), (m:Mountaineer)")
                                            .Where("id(h)=$hikeId and id(m)=$mountaineerId")
                                            .WithParams(dictParam)
                                            .Create("(m)-[c:Climb]->(h)")
                                            .ExecuteWithoutResultsAsync();
            }
            catch(Exception e)
            {
                _logger.LogError("Error adding race! " + e.Message);
            }
        }
        



    }

}
