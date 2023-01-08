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
                Guid id = Guid.NewGuid();
                Dictionary<string, object> dictParam = new Dictionary<string, object>();
                dictParam.Add("Id", id);
                dictParam.Add("Name", name);
                dictParam.Add("Surface", float.Parse(surface, CultureInfo.InvariantCulture.NumberFormat));
                
                obj =  await _graphClient.Cypher.Create("(n:Mountain{Id: $Id, name: $Name, surface: $Surface})")
                                                .WithParams(dictParam)
                                                .Return(n => n.As<Mountain>()).ResultsAsync;
                _logger.LogInformation("Mountain created successfully");
            }
            catch (Exception e)
            {
                _logger.LogError("Error creating mountain! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnMountain(string name)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(m:Mountain)")
                                                .Where((Mountain m) => m.name == name)
                                                .Return(m => m.As<Mountain>()).ResultsAsync;
            }
            catch(Exception e)
            {
                _logger.LogError("Error returning mountain");
            } 
            yield return obj;
        }
        public async IAsyncEnumerable<object> UpdateMountain(Guid id, Mountain mountain)
        {
            var obj = new object( );
            try
            {
                obj = await _graphClient.Cypher.Match("(m:Mountain)")
                                         .Where((Mountain m) => m.Id == id)
                                         .Set("m = $mountain")
                                         .WithParam("mountain", mountain)
                                         .Return(m => m.As<Mountain>()).ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error updating mountain! " + e.Message);
           
            }

            yield return obj;

        }
        public async IAsyncEnumerable<bool> DeleteMountain(Guid id)
        {
            bool done = true;
            try
            {
                await _graphClient.Cypher.Match("(m:Mountain)")
                                         .Where((Mountain m) => m.Id == id)
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
        public async IAsyncEnumerable<object> AddRegion(Guid mountainId, Guid regionId)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(m:Mountain), (reg: Region)")
                                                .Where((Mountain m, Region reg)=> m.Id == mountainId && reg.Id == regionId)
                                                .Create("(m)-[r:hasRegion]->(reg)")
                                                .Return(m => m.As<Mountain>()).ResultsAsync;
            }
            catch(Exception e)
            {
                _logger.LogError("Error adding region! " + e.Message);
            }
            yield return obj;
        } 

    }

}