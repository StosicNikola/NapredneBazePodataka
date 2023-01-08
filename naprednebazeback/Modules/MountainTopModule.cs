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
    public class MountainTopModules
    {
        private static IGraphClient _graphClient;
        private static ILogger _logger;

        public MountainTopModules(IGraphClient graphClient, ILogger logger)
        {
            _graphClient = graphClient;
            _logger = logger;
        }

        public async IAsyncEnumerable<object> CreateMountainTop(string name, string height)
        {
            var obj = new object();
            try
            {
                Guid id = Guid.NewGuid();
                Dictionary<string, object> dictParams = new Dictionary<string, object>();
                dictParams.Add("Id", id);
                dictParams.Add("Name", name);
                dictParams.Add("Height", height);
                obj = await _graphClient.Cypher.Create("(mt: MountainTop{Id:$Id, name:$Name,height:$Height})").WithParams(dictParams).Return(mt => mt.As<MountainTop>()).ResultsAsync;

            }
            catch (Exception e)
            {
                _logger.LogError("Error creating Mountain Top! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnMountainTopByName(string name)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(m:MountainTop)")
                .Where((MountainTop m) => m.name == name).Return(m => m.As<MountainTop>()).Limit(1).ResultsAsync;

            }
            catch (Exception e)
            {
                _logger.LogError("Error returning Mountain top! " + e.Message);

            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnMountainTopById(Guid id)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(m:MountainTop)")
                .Where((MountainTop m) => m.Id == id).Return(m => m.As<MountainTop>()).ResultsAsync;

            }
            catch (Exception e)
            {
                _logger.LogError("Error returning Mountain top! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> UpdateMountainTop(MountainTop newTop)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(m:MountainTop)")
                                                            .Where((MountainTop m) => m.Id == newTop.Id)
                                                            .Set("m=$mountainTop")
                                                            .WithParam("mountainTop", newTop)
                                                            .Return(m => m.As<MountainTop>())
                                                            .ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error updating mountain top! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> DeleteMountainTop(Guid id)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(m:MountainTop)")
                                        .Where((MountainTop m) => m.Id == id)
                                        .DetachDelete("m")
                                        .Return(m => m.As<MountainTop>())
                                        .ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error deleting Mountain Top! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> AddMountain(Guid mountainTopId, Guid mountainId)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(m:Mountain), (mt: MountainTop)")
                                                .Where((Mountain m, MountainTop mt) => m.Id == mountainId && mt.Id == mountainTopId)
                                                .Create("(mt)-[r:isLocated]->(m)")
                                                .Return(m => m.As<MountainTop>())
                                                .ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error adding mountain! " + e.Message);
            }
            yield return obj;
        }
    }

}