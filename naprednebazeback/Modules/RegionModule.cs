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
                Dictionary<string, object> dictParam = new Dictionary<string, object>();
                dictParam.Add("Name", name);
                obj = await _graphClient.Cypher.Create("(r:Region{Id:$Id, name:$Name})")
                                                .WithParam("Name",name)
                                                .With("r{.*, Id:id{r}} AS region")
                                                .Return(region=>region.As<Region>())
                                                .ResultsAsync;
            }
            catch(Exception e)
            {
                _logger.LogError("Error creating region! " + e.Message);
            }
            yield return obj;
        } 
        public async IAsyncEnumerable<object> ReturnAllRegions()
        {
            var obj = await _graphClient.Cypher.Match("(r:Region)")
                                                .With("r{.*, Id:id{r}} AS region")
                                                .Return(region=>region.As<Region>())
                                                .ResultsAsync;

            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnRegionByName(string name)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(r:Region)")
                                                .Where((Region r)=> r.name == name)
                                                .With("r{.*, Id:id{r}} AS region")
                                                .Return(region=>region.As<Region>())
                                                .ResultsAsync;
            }
            catch(Exception e)
            {
                _logger.LogError("Error returning region! " + e.Message);
            }
            yield return obj;

        }
        public async IAsyncEnumerable<object> ReturnRegionById(long id)
        {
            var obj = new object();
            try
            { 
                obj = await _graphClient.Cypher.Match("(r:Region)")
                                                .Where("id(r)=$Id")
                                                .WithParam("Id",id)
                                                .With("r{.*, Id:id{r}} AS region")
                                                .Return(region=>region.As<Region>())
                                                .ResultsAsync;
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
                obj = await _graphClient.Cypher.Match("(r:Region)")
                                                .Where("id(r)=$Id")
                                                .WithParam("Id",region.Id)
                                                .Set("r=$region")
                                                .WithParam("region", new
                                                {
                                                    region.name
                                                })
                                                .With("r{.*, Id:id{r}} AS region")
                                                .Return(region=>region.As<Region>())
                                                .ResultsAsync;
            }
            catch(Exception e)
            {
                _logger.LogError("Error updating region! " + e.Message);
            }
            yield return obj;
        }   
        public async IAsyncEnumerable<object> DeleteRegion(long id)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(r:Region)")
                                                .Where("id(r)=$Id")
                                                .WithParam("Id",id)
                                                .DetachDelete("r")
                                                .With("r{.*, Id:id{r}} AS region")
                                                .Return(region=>region.As<Region>())
                                                .ResultsAsync;
            }
            catch(Exception e)
            {
                _logger.LogError("Error deleting region! " + e.Message );
            }
            yield return obj;
        }

    }
}