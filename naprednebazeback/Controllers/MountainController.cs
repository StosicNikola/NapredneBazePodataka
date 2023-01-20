using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using naprednebazeback.ObjectModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neo4jClient;
using Neo4jClient.Cypher;
using naprednebazeback.Modules;

namespace naprednebazeback.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class MountainController:ControllerBase
    {
        private static IGraphClient _graphClient;
        private static ILogger<MountainController> _logger;
        private static MountainModule _modules;
        public MountainController(IGraphClient graphClient, ILogger<MountainController> logger)
        {
            _modules = new MountainModule(graphClient,logger );
            _graphClient = graphClient;
            _logger = logger;
        }

        [HttpPost]
        [Route("{name}/{surface}")]
        public async Task<ActionResult> CreateMountain(string name, float surface )
        {
            return Ok( _modules.CreateMountain(name,surface));
        }
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> ReturnAllMountains()
        {
            return Ok(_modules.ReturnAllMountains());
        }
        [HttpGet]
        [Route("id/{id}")]
        public async Task<ActionResult> ReturnMountainById(long id)
        {
            return Ok(_modules.ReturnMountainById(id));
        }
        [HttpGet]
        [Route("name/{name}")]
        public async Task<ActionResult> ReturnMountain(string name)
        {
            return Ok( _modules.ReturnMountainByName(name));
        }
        [HttpPut]
        [Route("")]
        public async Task<ActionResult> UpdateMountain([FromBody]Mountain m)
        {
            return Ok(_modules.UpdateMountain(m));
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteMountain(long id)
        {
            return Ok(_modules.DeleteMountain(id));
        }
        [HttpPost]
        [Route("{mountainId}/region/{regionId}")]
        public async Task<ActionResult> CreateRelationship(long mountainId, long regionId)
        {
            return Ok(_modules.AddRegion(mountainId,regionId));
        }
        [HttpGet]
        [Route("region/{regionId}")]
        public async Task<ActionResult> ReturnMountainInRegion(long regionId)
        {
            return Ok(_modules.ReturnMountainInRegion(regionId));
        }
       

    }

}