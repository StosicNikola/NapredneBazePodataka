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

                Dictionary<string, object> dictParams = new Dictionary<string, object>();
                dictParams.Add("Name", name);
                dictParams.Add("Height", height);
                obj = await _graphClient.Cypher.Create("(m: MountainTop{ name:$Name,height:$Height})")
                                                .WithParams(dictParams)
                                                .With("m{.*, Id:id(m)} AS mountainTop")
                                                .Return(mountainTop => mountainTop.As<MountainTop>())
                                                .ResultsAsync;

            }
            catch (Exception e)
            {
                _logger.LogError("Error creating Mountain Top! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnMountainTops()
        {
            var obj = await _graphClient.Cypher.Match("(m:MountainTop)")
                                                .With("m{.*, Id:id(m)} as mountainTop" )
                                                .Return(mountainTop=>mountainTop.As<MountainTop>())
                                                .ResultsAsync;
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnMountainTopByName(string name)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(m:MountainTop)")
                                                .Where((MountainTop m) => m.name == name)
                                                .With("m{.*, Id:id(m)} AS mountainTop")
                                                .Return(mountainTop => mountainTop.As<MountainTop>())
                                                .ResultsAsync;

            }
            catch (Exception e)
            {
                _logger.LogError("Error returning Mountain top! " + e.Message);

            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnMountainTopById(long id)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(m:MountainTop)")
                                                .Where("id(m)=$id")
                                                .WithParam("id",id)
                                                .With("m{.*, Id:id(m)} AS mountainTop")
                                                .Return(mountainTop => mountainTop.As<MountainTop>())
                                                .ResultsAsync;

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
                                                .Where("id(m)=$id")
                                                .WithParam("id",newTop.Id)
                                                .Set("m=$mountainTop")
                                                .WithParam("mountainTop", new{
                                                    newTop.name,
                                                    newTop.height
                                                })
                                                .With("m{.*, Id:id(m)} AS mountainTop")
                                                .Return(mountainTop => mountainTop.As<MountainTop>())
                                                .ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error updating mountain top! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> DeleteMountainTop(long id)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(m:MountainTop)")
                                        .Where("id(m)=$Id")
                                        .WithParam("Id",id)
                                        .DetachDelete("m")
                                        .With("m{.*, Id:id(m)} AS mountainTop")
                                        .Return(mountainTop => mountainTop.As<MountainTop>())
                                        .ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error deleting Mountain Top! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> AddMountain(long mountainTopId, long mountainId)
        {
            var obj = new object();
            try
            {
                Dictionary<string, object> dictParam = new Dictionary<string, object>();
                dictParam.Add("mountainId",mountainId);
                dictParam.Add("mountainTopId",mountainTopId);
                obj = await _graphClient.Cypher.Match("(m:Mountain), (mt: MountainTop)")
                                                .Where("id(m)=$mountainId and id(mt)=$mountainTopId")
                                                .WithParams(dictParam)
                                                .Create("(mt)-[r:isLocatedOn]->(m)")
                                                .With("m{.*, Id:id(m)} AS mountainTop")
                                                .Return(mountainTop => mountainTop.As<MountainTop>())
                                                .ResultsAsync;
            }
            catch (Exception e)
            {
                _logger.LogError("Error adding mountain! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnMountainTopsOnMountain(long mountainId)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(m:Mountain)<-[r:isLocatedOn]-(mt:MountainTop)")
                                                .Where("id(m)=$Id")
                                                .WithParam("Id",mountainId)
                                                .With("mt{.*, Id:id(mt)} AS mountainTop")
                                                .Return(mountainTop=>mountainTop.As<MountainTop>())
                                                .ResultsAsync;
            }
            catch(Exception e)
            {
                _logger.LogError("Error returnig mountain tops! " + e.Message);
            }
            yield return obj;
        }
        public async IAsyncEnumerable<object> ReturnMountainTopsInRegion(long regionId)
        {
            var obj = new object();
            try
            {
                obj = await _graphClient.Cypher.Match("(r:Region)<-[p:hasRegion]-(m:Mountain)<-[k:isLocatedOn]-(mt:MountainTop)")
                                                .Where("id(r)=$Id")
                                                .WithParam("Id",regionId)
                                                .With("mt{.*, Id:id(mt)} AS mountainTop")
                                                .ReturnDistinct(mountainTop=>mountainTop.As<MountainTop>())
                                                .ResultsAsync;
            }
            catch(Exception e)
            {
                _logger.LogError("Error returnig mountain tops! " + e.Message);
            }
            yield return obj;
        }
    }

}