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
        [Route("hike/{name}/{date}/{difficulty}/{about}")]
        public async Task<ActionResult> CreateHike(string name, DateTime date, int difficulty,string about)
        {
            return Ok(_module.CreateHikeEvent(name, date, difficulty,about));
        }
        [HttpPost]
        [Route("hike/{name}/{date}/{difficulty}/{about}/{idMountainTop}/{idHikingguide}")]
        public async Task<ActionResult> CreateHikeAllData(string name, DateTime date, int difficulty, string about,long idMountainTop,long idHikingguide)
        {
            return Ok(_module.CreateHikeEventAllData(name, date, difficulty,about,idMountainTop ,idHikingguide));
        }
        [HttpPost]
        [Route("race/{name}/{date}/{difficulty}/{about}")]
        public async Task<ActionResult> CreateRace(string name, DateTime date, int difficulty,string about)
        {
            return Ok(_module.CreateRaceEvent(name, date, difficulty,about));
        }
        [HttpPost]
        [Route("race/{name}/{date}/{difficulty}/{about}/{idMountainTop}/{idReferee}")]
        public async Task<ActionResult> CreateRaceAllData(string name, DateTime date, int difficulty, string about,long idMountainTop,long idReferee)
        {
            return Ok(_module.CreateRaceEventAllData(name, date, difficulty,about,idMountainTop ,idReferee));
        }
        [HttpGet]
        [Route("event/{eventName}")]
        public async Task<ActionResult> ReturnEvent(string eventName)
        {
            return Ok(_module.ReturnEvent(eventName));
        }
        
        [HttpGet]
        [Route("hike")]
        public async Task<ActionResult> ReturnAllHikeEvents()
        {
            return Ok(_module.ReturnAllHikeEvents());
        }
        [HttpGet]
        [Route("race")]
        public async Task<ActionResult> ReturnAllRaceEvents()
        {
            return Ok(_module.ReturnAllRaceEvents());
        }
        [HttpGet]
        [Route("hike/id/{id}")]
        public async Task<ActionResult> ReturnHikeById(long id)
        {
            return Ok(_module.ReturnHikeEventById(id));
        }
        [HttpGet]
        [Route("hike/name/{name}")]
        public async Task<ActionResult> ReturnHikeByName(string name)
        {
            return Ok(_module.ReturnHikeEventByName(name));
        }
        [HttpGet]
        [Route("race/id/{id}")]
        public async Task<ActionResult> ReturnRaceById(long id)
        {
            return Ok(_module.ReturnRaceEventById(id));
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
        public async Task<ActionResult> DeleteHike(long id)
        {
            return Ok(_module.DeleteHike(id));
        }
        [HttpDelete]
        [Route("race/{id}")]
        public async Task<ActionResult> DeleteRace(long id)
        {
            return Ok(_module.DeleteRace(id));
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteEvent(long id)
        {
            return Ok(_module.DeleteEvent(id));
        }
        [HttpPost]
        [Route("hike/{hikeId}/mountaintop/{mountainTopId}")]
        public async Task<ActionResult> AddMountainTopForHike(long hikeId,long mountainTopId)
        {
            return Ok(_module.AddMountainTopForHike(mountainTopId, hikeId));
        }
        [HttpPost]
        [Route("race/{raceId}/mountaintop/{mountainTopId}")]
        public async Task<ActionResult> AddMountainTopForRace(long raceId,long mountainTopId)
        {
            return Ok(_module.AddMountainTopForRace(mountainTopId, raceId));
        }

        
    }
}