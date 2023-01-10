using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using naprednebazeback.ObjectModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using naprednebazeback.RedisDataLayer;
using naprednebazeback.DTOs;

namespace naprednebazeback.Controllers
{
     [ApiController]
    [Route("[controller]")]
    public class RedisLeaderboardControllers : ControllerBase
    {
        private static RedisDao rd;

        public RedisLeaderboardControllers(ILogger<EventController> logger )
        {
            rd = new RedisDao(logger);
        }

        [HttpPost]
        [Route("CreateLeaderBoardPerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateLeaderBoardPerson([FromBody]MountainRunnerView mrv)
        {
            try
            {
                rd. AddRunnerToSet(mrv);
                //logika za prikupljanje podataka o Person
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("GetMainleaderboardFrom0toNitems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetMainleaderboardFrom0toNitems(int numOfReturnItems)
        {
            try
            {
                List<MountainRunnerView> runner = rd.GetMainLeaderboardFrom0toNPeople(numOfReturnItems);
                return new JsonResult(runner);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("GetLeaderboardForPersonFrom0toNitems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetLeaderboardForPersonFrom0toNitems(int numOfReturnItems, long personId)
        {
            try
            {
                List<MountainRunnerView> runner = rd.GetLeaderboardForPersonFrom0toNitems(numOfReturnItems, personId);
                return new JsonResult(runner);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

         [HttpGet]
        [Route("GetDictionary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetDictionary()
        {
            try
            {
                List<string> retu = new List<string>();
                Dictionary<long,string> dict = rd.GetDictionary();
                foreach(KeyValuePair<long, string> kvp in dict){
                    retu.Add("Key = " + kvp.Key + " Value = "+ kvp.Value);
                }

                return new JsonResult(dict);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }



        [HttpDelete]
        [Route("DeleteAllItemInDb")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteAllItemInDb()
        {
            try
            {
                rd.DeleteAllItemInDb();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}