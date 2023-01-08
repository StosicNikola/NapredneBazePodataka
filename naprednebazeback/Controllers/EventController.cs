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
    public class EventController:ControllerBase
    {
        private static EventModule _module;
        public EventController(IGraphClient graphClient, ILogger<EventController> logger )
        {
            _module = new EventModule(graphClient, logger);
        }

        [HttpPost]
        [Route("hike/{name}/{date}/{difficulty}")]
        public async Task<ActionResult> CreateHike(string name, DateTime date, int difficulty)
        {
            return Ok(_module.CreateHikeEvent(name, date, difficulty));
        }
        [HttpPost]
        [Route("race/{name}/{date}/{difficulty}")]
        public async Task<ActionResult> CreateRace(string name, DateTime date, int difficulty)
        {
            return Ok(_module.CreateRaceEvent(name, date, difficulty));
        }
        [HttpGet]
        [Route("hike/id/{id}")]
        public async Task<ActionResult> ReturnHikeById(string id)
        {
            return Ok(_module.ReturnHikeEventById(new Guid(id)));
        }
        [HttpGet]
        [Route("hike/name/{name}")]
        public async Task<ActionResult> ReturnHikeByName(string name)
        {
            return Ok(_module.ReturnHikeEventByName(name));
        }
        [HttpGet]
        [Route("race/id/{id}")]
        public async Task<ActionResult> ReturnRaceById(string id)
        {
            return Ok(_module.ReturnRaceEventById(new Guid(id)));
        }
        [HttpGet]
        [Route("race/name/{name}")]
        public async Task<ActionResult> ReturnRaceByName(string name)
        {
            return Ok(_module.ReturnRaceEventByName(name));
        }
        [HttpPut]
        [Route("hike")]
        public async Task<ActionResult> UpdateHike([FromBody]Hike hike)
        {
            return Ok(_module.UpdateHike(hike));
        }
        [HttpPut]
        [Route("race")]
        public async Task<ActionResult> UpdateRace([FromBody]Race race)
        {
            return Ok(_module.UpdateRace(race));
        }
        [HttpDelete]
        [Route("hike/{id}")]
        public async Task<ActionResult> DeleteHike(string id)
        {
            return Ok(_module.DeleteHike(new Guid(id)));
        }
        [HttpDelete]
        [Route("race/{id}")]
        public async Task<ActionResult> DeleteRace(string id)
        {
            return Ok(_module.DeleteRace(new Guid(id)));
        }
        [HttpPost]
        [Route("hike/{hikeId}/mountaintop/{mountainTopId}")]
        public async Task<ActionResult> AddMountainTopForHike(string hikeId,string mountainTopId)
        {
            return Ok(_module.AddMountainTopForHike(new Guid(mountainTopId), new Guid(hikeId)));
        }
        [HttpPost]
        [Route("race/{raceId}/mountaintop/{mountainTopId}")]
        public async Task<ActionResult> AddMountainTopForRace(string raceId,string mountainTopId)
        {
            return Ok(_module.AddMountainTopForRace(new Guid(mountainTopId), new Guid(raceId)));
        }

        
    }
}