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


namespace back.Modules
{
    public class MountainModule
    {
        private static IGraphClient _graphClient;
        private static ILogger _logger;

        public MountainModule(IGraphClient graphClient, ILogger logger)
        {
            _graphClient = graphClient;
            _logger = logger;
        }
        public async IAsyncEnumerable<object> CreateMountain(string name, string surface)
        {
            var obj = new object();
            try
            {
                Dictionary<string, object> dictParam = new Dictionary<string, object>();
                dictParam.Add("Name", name);
                dictParam.Add("Surface", float.Parse(surface, CultureInfo.InvariantCulture.NumberFormat));
                obj =  await _graphClient.Cypher.Create("(n:Mountain{name: $Name, surface: $Surface})")
                                                .WithParams(dictParam)
                                                .With("m{.*, Id:id(m)} AS mountain")
                                                .Return(mountain => mountain.As<Mountain>())
                                                .ResultsAsync;
                _logger.LogInformation("Mountain created successfully");
            }
            catch (Exception e)
            {
                _logger.LogError("Error creating mountain! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnAllMountains()
        {
            var obj = await _graphClient.Cypher.Match("m:Mountain")
                                                .With("m{.*, Id:id(m)} AS mountain")
                                                .Return(mountain => mountain.As<Mountain>())
                                                .ResultsAsync;
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnMountainById(long id)
        {
            var obj = await _graphClient.Cypher.Match("(m:Mountain)")
                                                .Where("id(m)= $Id")
                                                .WithParam("Id",id)
                                                .With("m{.*, Id:id(m)} AS mountain")
                                                .Return(mountain => mountain.As<Mountain>())
                                                .ResultsAsync;
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnMountainByName(string name)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(m:Mountain)")
                                                .Where((Mountain m) => m.name == name)
                                                .With("m{.*, Id:id(m)} AS mountain")
                                                .Return(mountain => mountain.As<Mountain>())
                                                .ResultsAsync;
            }
            catch(Exception e)
            {
                _logger.LogError("Error returning mountain");
            } 
            yield return obj;
        }
       
        public async IAsyncEnumerable<object> UpdateMountain( Mountain mountain)
        {
            var obj = new object( );
            try
            {
                obj = await _graphClient.Cypher.Match("(m:Mountain)")
                                         .Where("id(m)=$id")
                                         .WithParam("id",mountain.Id)
                                         .Set("m = $mountain")
                                         .WithParam("mountain", new{
                                            mountain.name,
                                            mountain.surface
                                         })
                                         .With("m{.*, Id:id(m)} AS mountain")
                                         .Return(mountain => mountain.As<Mountain>())
                                         .ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error updating mountain! " + e.Message);
           
            }

            yield return obj;

        }
        public async IAsyncEnumerable<bool> DeleteMountain(long id)
        {
            bool done = true;
            try
            {
                await _graphClient.Cypher.Match("(m:Mountain)")
                                         .Where("id(m)=$id")
                                         .WithParam("id",id)
                                         .DetachDelete("m")
                                         .ExecuteWithoutResultsAsync();

            }
            catch (Exception e)
            {
                _logger.LogError("Error deleting Mountain! " + e.Message);
                done = false;
            }
            yield return done;
        }
        public async IAsyncEnumerable<object> AddRegion(long mountainId, long regionId)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(m:Mountain), (reg: Region)")
                                                .Where("id(m)=$mountainId and id(reg)=$regionId")
                                                .WithParam("mountainId",mountainId)
                                                .WithParam("regionId",regionId)
                                                .Create("(m)-[r:hasRegion]->(reg)")
                                                .With("m{.*, Id:id(m)} AS mountain")
                                                .Return(mountain => mountain.As<Mountain>())
                                                .ResultsAsync;
            }
            catch(Exception e)
            {
                _logger.LogError("Error adding region! " + e.Message);
            }
            yield return obj;
        } 

    }

}