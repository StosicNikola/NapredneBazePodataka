using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using naprednebazeback.ObjectModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neo4jClient;
using naprednebazeback.Modules;
using Neo4jClient.Cypher;

namespace naprednebazeback.Controllers
{
    public class RegionController:ControllerBase
    {
        private static RegionModule _module;
        public RegionController(IGraphClient graphClient, ILogger<RegionController> logger)
        {
            _module = new RegionModule(graphClient,logger);
        }
        [HttpGet]
        [Route("id/{id}")]
        public async Task<ActionResult> ReturnRegionById(long id)
        {
            return Ok(_module.ReturnRegionById(id));
        }
        [HttpGet]
        [Route("name/{name}")]
        public async Task<ActionResult> ReturnRegionByName(string name)
        {
            return Ok(_module.ReturnRegionByName(name));
        }
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> ReturnAllRegions()
        {
            return Ok(_module.ReturnAllRegions());
        }
        [HttpPost]
        [Route("{name}")]
        public async Task<ActionResult> CreateRegion(string name)
        {
            return Ok(_module.CreateRegion(name));
        }
        [HttpPut]
        [Route("")]
        public async Task<ActionResult> UpdateRegion(string id,  [FromBody]Region region)
        {
            return Ok(_module.UpdateRegion(region));
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteRegion(long id)
        {
            _module.DeleteRegion(id);
            return Ok();
        }
        
    }
}