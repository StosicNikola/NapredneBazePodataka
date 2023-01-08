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
    public class RegionModule
    {
        private static IGraphClient _graphClient;
        private static ILogger _logger;
        public RegionModule(IGraphClient graphClient, ILogger logger)
        {
            _graphClient = graphClient;
            _logger = logger;
        }
        public async IAsyncEnumerable<object> CreateRegion(string name)
        {
            var obj = new object();
            try
            {
                Guid id = Guid.NewGuid();
                Dictionary<string, object> dictParam = new Dictionary<string, object>();
                dictParam.Add("Id", id);
                dictParam.Add("Name", name);
                obj = await _graphClient.Cypher.Create("(r:Region{Id:$Id, name:$Name})").WithParams(dictParam).Return(r=>r.As<Region>()).ResultsAsync;
            }
            catch(Exception e)
            {
                _logger.LogError("Error creating region! " + e.Message);
            }
            yield return obj;
        } 
        public async IAsyncEnumerable<object> ReturnRegionByName(string name)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(r:Region)").Where((Region r)=> r.name == name).Return(r=>r.As<Region>()).ResultsAsync;
            }
            catch(Exception e)
            {
                _logger.LogError("Error returning region! " + e.Message);
            }
            yield return obj;

        }
        public async IAsyncEnumerable<object> ReturnRegionById(Guid id)
        {
            var obj = new object();
            try
            { 
                obj = await _graphClient.Cypher.Match("(r:Region)").Where((Region r)=>r.Id == id).Return(r=>r.As<Region>()).ResultsAsync;
            }
            catch(Exception e)
            {
                _logger.LogError("Error returning region! " + e.Message);
            }
             yield return obj;
        }
        public async IAsyncEnumerable<object> UpdateRegion( Region region)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(r:Region)").Where((Region r)=> r.Id == region.Id).Set("r=$region").WithParam("region", region).Return(r=>r.As<Region>()).ResultsAsync;
            }
            catch(Exception e)
            {
                _logger.LogError("Error updating region! " + e.Message);
            }
            yield return obj;
        }   
        public async IAsyncEnumerable<object> DeleteRegion(Guid id)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(r:Region)").Where((Region r)=> r.Id==id).DetachDelete("r").Return(r=>r.As<Region>()).ResultsAsync;
            }
            catch(Exception e)
            {
                _logger.LogError("Error deleting region! " + e.Message );
            }
            yield return obj;
        }

    }
}