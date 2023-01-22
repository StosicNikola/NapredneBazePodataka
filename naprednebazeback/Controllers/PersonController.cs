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

namespace naprednebazeback.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController:ControllerBase
    {   
        private static PersonModule _module;
        public PersonController(IGraphClient graphClient, ILogger<PersonController> logger)
        {   
            _module = new PersonModule(graphClient, logger);
        }

        [HttpPost]
        [Route("admin/{name}/{surname}/{age}/{accountId}")]
        public async Task<ActionResult> CreateAdmin(string name, string surname, int age,long accountId)
        {
            return Ok(_module.CreateAdmin(name,surname,age,accountId));
        }


        [HttpPost]
        [Route("mountaineer/{name}/{surname}/{age}/{memberCard}/{accountId}")]
        public async Task<ActionResult> CreateMounatineer(string name, string surname, int age, long memberCard, long accountId)
        {
           
            return Ok( _module.CreateMountaineer(name,surname,age,memberCard,accountId));
        }

        
        [HttpPost]
        [Route("hikingguide/{name}/{surname}/{age}/{licenseNumber}/{accountId}")]
        public async Task<ActionResult> CreateHikingGuide(string name, string surname, int age, long licenseNumber,long accountId)
        {
            return Ok(_module.CreateHikingGuide(name,surname,age,licenseNumber,accountId));
        }

        
        [HttpPost]
        [Route("referee/{name}/{surname}/{age}/{accountId}")]
        public async Task<ActionResult> CreateReferee(string name, string surname, int age,long accountId)
        {
            return Ok(_module.CreateReferee(name,surname,age,accountId));
        }
        [HttpGet]
        [Route("{personId}")]
        public async Task<ActionResult> ReturnPerson(long personId)
        {
            return Ok(_module.ReturnPerson(personId));
        }
        [HttpGet]
        [Route("referee")]
        public async Task<ActionResult> ReturnAllReferees()
        {
            return Ok(_module.ReturnAllReferees());
        }
        [HttpGet]
        [Route("mountaineer")]
        public async Task<ActionResult> ReturnAllMountaineers()
        {
           return Ok(_module.ReturnAllReferees());
        }
        [HttpGet]
        [Route("hikingguide")]
        public async Task<ActionResult> ReturnAllHikingGuids()
        {
           return Ok(_module.ReturnAllHikingGuids());
        }
         [HttpGet]
        [Route("referee/name/{name}")]
        public async Task<ActionResult> ReturnRefereeByName(string name)
        {
           return Ok(_module.ReturnRefereeByName(name));
        }
        [HttpGet]
        [Route("referee/id/{id}")]
        public async Task<ActionResult> ReturnRefereeById(long id)
        {
           return Ok(_module.ReturnRefereeById(id));
        }
        [HttpGet]
        [Route("mountaineer/name/{name}")]
        public async Task<ActionResult> ReturnMounatineerByName(string name)
        {
           return Ok(_module.ReturnMounatineerByName(name));
        }
        [HttpGet]
        [Route("mountaineer/id/{id}")]
        public async Task<ActionResult> ReturnMounatineerById(long id)
        {
           return Ok(_module.ReturnMounatineerById(id));
        }
        [HttpGet]
        [Route("hikingguide/name/{name}")]
        public async Task<ActionResult> ReturnHikingGuideByName(string name)
        {
           return Ok(_module.ReturnHikingGuideByName(name));
        }
        [HttpGet]
        [Route("hikingguide/id/{id}")]
        public async Task<ActionResult> ReturnHikingGuideById(long id)
        {
           return Ok(_module.ReturnHikingGuideById(id));
        }
        [HttpPut]
        [Route("referee")]
        public async Task<ActionResult> UpdateReferee([FromBody]Person referee)
        {
            return Ok(_module.UpdateReferee(referee));
        }
        [HttpPut]
        [Route("mountaineer")]
        public async Task<ActionResult> UpdateMountaineer([FromBody]Person mountaineer)
        {
            return Ok(_module.UpdateMountaineer(mountaineer));
        }
        [HttpPut]
        [Route("hikingguide")]
        public async Task<ActionResult> UpdateHikingGuide([FromBody]Person hikingGuide)
        {
            return Ok(_module.UpdateHikingGuide(hikingGuide));
        }
        [HttpDelete]
        [Route("referee/{id}")] 
        public async Task<ActionResult> DeleteReferee(long id)
        {
            _module.DeleteReferee(id);
            return Ok();
        }
        [HttpDelete]
        [Route("mountaineer/{id}")] 
        public async Task<ActionResult> DeleteMountaineer(long id)
        {
            _module.DeleteMountaineer(id);
            return Ok();
        }
        [HttpDelete]
        [Route("hikingguide/{id}")] 
        public async Task<ActionResult> DeleteHikingGuide(long id)
        {
            _module.DeleteHikingGuide(id);
            return Ok();
        }
        [HttpPost]
        [Route("hikingguide/{hikeGuideId}/hike/{hikeId}")]
        public async Task<ActionResult> AddHike(long hikeGuideId , long hikeId)
        {
            _module.AddHike(hikeId, hikeGuideId);
            return Ok();
        }
        [HttpPost]
        [Route("referee/{refereeId}/race/{raceId}")]
        public async Task<ActionResult> AddRace(long refereeId, long raceId)
        {
            _module.AddRace(raceId, refereeId);
            return Ok();
        }
        [HttpPost]
        [Route("mountaineer/{mountaineerId}/race/{raceId}")]
        public async Task<ActionResult> AddMountaineerCompete(long mountaineerId, long raceId)
        {
            _module.AddMountaineerCompete(raceId, mountaineerId);
            return Ok();
        }
        [HttpPost]
        [Route("mountaineer/{mountaineerId}/hike/{hikeId}")]
        public async Task<ActionResult> AddMountaineerClimbs(long mountaineerId, long hikeId)
        {
            _module.AddMountaineerClimbs(hikeId,mountaineerId);
            return Ok();
        }





    }
}