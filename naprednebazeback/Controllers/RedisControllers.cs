using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using naprednebazeback.ObjectModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using naprednebazeback.RedisDataLayer;

namespace naprednebazeback.Controllers
{
     [ApiController]
    [Route("[controller]")]
    public class RedisControllers : ControllerBase
    {
        [HttpGet]
        [Route("CreateRedisObject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateRedisObjectg()
        {
            try
            {
                RedisDao rd = new RedisDao();
                rd. AddRunnerToSet("Nikola",DateTime.Now,5);
                string result = rd.GetFirstLeader();
                return new JsonResult(result);
               // return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}