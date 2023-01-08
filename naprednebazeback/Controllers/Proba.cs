using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using naprednebazeback.ObjectModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neo4jClient;

namespace naprednebazeback.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProbaController : ControllerBase
    {
        private IGraphClient _neo4jClient;
        public ProbaController(IGraphClient neo4jCLient)
        {
            _neo4jClient = neo4jCLient;
        }

        [HttpGet]
        [Route("proba")]
        public async Task<ActionResult> getIme()
        {
            var obj = await _neo4jClient.Cypher.Match("(n:Region)").Return(n=>n.As<Region>()).ResultsAsync;
            try{
                return Ok(obj);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
            //  var departments = await _client.Cypher.Match("(n: Department)")
            //                                        .Return(n => n.As<Department>()).ResultsAsync;

            // return Ok(departments);
        }
        [HttpPost]
        [Route("proba")]
        public async Task<ActionResult> setIme()
        {
            await _neo4jClient.Cypher.Create("(n:Region)").WithParam("Id",1).ExecuteWithoutResultsAsync();
            try{
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
             
           
        }
    }

}