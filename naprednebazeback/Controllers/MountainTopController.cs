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
    [ApiController]
    [Route("[controller]")]
    public class MountainTopController : ControllerBase
    {

        private MountainTopModules _modules;
        public MountainTopController(IGraphClient graphClient, ILogger<MountainTopController> logger)
        {
            _modules = new MountainTopModules(graphClient,logger);
        }
        [HttpPost]
        [Route("{name}/{height}")]
        public async Task<ActionResult> SetMountainTop(string name, string height)
        {
            var tmp = _modules.CreateMountainTop(name,height);
            return Ok(tmp);
        }
        [HttpGet]
        [Route("id/{id}")]
        public async Task<ActionResult> GetMountainTopById(long id)
        {
            return Ok(_modules.ReturnMountainTopById(id));
        }
        [HttpGet]
        [Route("name/{name}")]
        public async Task<ActionResult> GetMountainTopByName(string name)
        {
            return Ok(_modules.ReturnMountainTopByName(name));
        }
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> GetAllMountainTops()
        {
            return Ok(_modules.ReturnMountainTops());
        }
        [HttpPut]
        [Route("")]
        public async Task<ActionResult> UpdateMountainTop([FromBody]MountainTop top)
        {
            return Ok(_modules.UpdateMountainTop(top));
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteMoutainTop(long id)
        {
            return Ok(_modules.DeleteMountainTop(id));
        }
        [HttpPost]
        [Route("{mountainTopId}/mountain/{mountainId}")]
        public async Task<ActionResult> AddMountain(long mountainTopId, long mountainId)
        {
            return Ok(_modules.AddMountain(mountainTopId, mountainId));
        }
         [HttpGet]
        [Route("mountain/{mountainId}/")]
        public async Task<ActionResult> ReturnMountainTops(long mountainId)
        {
            return Ok(_modules.ReturnMountainTopsOnMountain(mountainId));
        }
        [HttpGet]
        [Route("region/{regionId}/")]
        public async Task<ActionResult> ReturnMountainTopsInRegion(long regionId)
        {
            return Ok(_modules.ReturnMountainTopsInRegion(regionId));
        }
    
    }
}