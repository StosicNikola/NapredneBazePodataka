using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using naprednebazeback.ObjectModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neo4jClient;
using back.Modules;
using Neo4jClient.Cypher;


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
        public async Task<ActionResult> CreateMountain(string name, string surface )
        {
            return Ok( _modules.CreateMountain(name,surface));
        }
        [HttpGet]
        [Route("{name}")]
        public async Task<ActionResult> ReturnMountain(string name)
        {
            return Ok( _modules.ReturnMountain(name));
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateMountain(string id,[FromBody]Mountain m)
        {
            return Ok(_modules.UpdateMountain(new Guid(id),m));
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteMountain(string id)
        {
            return Ok(_modules.DeleteMountain(new Guid(id)));
        }
        [HttpPost]
        [Route("{mountainId}/region/{regionId}")]
        public async Task<ActionResult> CreateRelationship(string mountainId, string regionId)
        {
            return Ok(_modules.AddRegion(new Guid(mountainId),new Guid(regionId)));
        }

    }

}