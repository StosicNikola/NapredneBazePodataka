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
        public async Task<ActionResult> GetMountainTopById(string id)
        {
            return Ok(_modules.ReturnMountainTopById(new Guid(id)));
        }
        [HttpGet]
        [Route("name/{name}")]
        public async Task<ActionResult> GetMountainTopByName(string name)
        {
            return Ok(_modules.ReturnMountainTopByName(name));
        }
        [HttpPut]
        [Route("")]
        public async Task<ActionResult> UpdateMountainTop( [FromBody]MountainTop top)
        {
            return Ok(_modules.UpdateMountainTop(top));
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteMoutainTop(string id)
        {
            return Ok(_modules.DeleteMountainTop(new Guid(id)));
        }
        [HttpPost]
        [Route("{mountainTopId}/mountain/{mountainId}")]
        public async Task<ActionResult> AddMountain(string mountainTopId, string mountainId)
        {
            return Ok(_modules.AddMountain(new Guid(mountainTopId), new Guid(mountainId)));
        }
    }
}