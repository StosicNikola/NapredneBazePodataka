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
      /*  [HttpGet]
        [Route("referee/id/{id}")]
        public async Task<ActionResult> ReturnRefereeById(string id)
        {
           return Ok(_module.ReturnRefereeById(new Guid(id)));
        }*/
        [HttpGet]
        [Route("mountaineer/name/{name}")]
        public async Task<ActionResult> ReturnMounatineerByName(string name)
        {
           return Ok(_module.ReturnMounatineerByName(name));
        }
      /*  [HttpGet]
        [Route("mountaineer/id/{id}")]
        public async Task<ActionResult> ReturnMounatineerById(string id)
        {
           return Ok(_module.ReturnMounatineerById(new Guid(id)));
        }*/
        [HttpGet]
        [Route("hikingguide/name/{name}")]
        public async Task<ActionResult> ReturnHikingGuideByName(string name)
        {
           return Ok(_module.ReturnHikingGuideByName(name));
        }
      /*  [HttpGet]
        [Route("hikingguide/id/{id}")]
        public async Task<ActionResult> ReturnHikingGuideById(string id)
        {
           return Ok(_module.ReturnHikingGuideById(new Guid(id)));
        }*/
        [HttpPut]
        [Route("referee")]
        public async Task<ActionResult> UpdateReferee([FromBody]Referee referee)
        {
            return Ok(_module.UpdateReferee(referee));
        }
        [HttpPut]
        [Route("mountaineer")]
        public async Task<ActionResult> UpdateMountaineer([FromBody]Mountaineer mountaineer)
        {
            return Ok(_module.UpdateMountaineer(mountaineer));
        }
        [HttpPut]
        [Route("hikingguide")]
        public async Task<ActionResult> UpdateHikingGuide([FromBody]HikingGuide hikingGuide)
        {
            return Ok(_module.UpdateHikingGuide(hikingGuide));
        }
    /*    [HttpDelete]
        [Route("referee/{id}")] 
        public async Task<ActionResult> DeleteReferee(string id)
        {
            _module.DeleteReferee(new Guid(id));
            return Ok();
        }
        [HttpDelete]
        [Route("mountaineer/{id}")] 
        public async Task<ActionResult> DeleteMountaineer(string id)
        {
            _module.DeleteMountaineer(new Guid(id));
            return Ok();
        }
        [HttpDelete]
        [Route("hikingguide/{id}")] 
        public async Task<ActionResult> DeleteHikingGuide(string id)
        {
            _module.DeleteHikingGuide(new Guid(id));
            return Ok();
        }
        [HttpPost]
        [Route("hikingguide/{hikeGuideId}/hike/{hikeId}")]
        public async Task<ActionResult> AddHike(string hikeGuideId , string hikeId)
        {
            _module.AddHike(new Guid(hikeId), new Guid(hikeGuideId));
            return Ok();
        }
        [HttpPost]
        [Route("referee/{refereeId}/race/{raceId}")]
        public async Task<ActionResult> AddRace(string refereeId, string raceId)
        {
            _module.AddRace(new Guid(raceId), new Guid(refereeId));
            return Ok();
        }
        [HttpPost]
        [Route("mountaineer/{mountaineerId}/race/{raceId}")]
        public async Task<ActionResult> AddMountaineerCompete(string mountaineerId, string raceId)
        {
            _module.AddMountaineerCompete(new Guid(raceId), new Guid(mountaineerId));
            return Ok();
        }
        [HttpPost]
        [Route("mountaineer/{mountaineerId}/hike/{hikeId}")]
        public async Task<ActionResult> AddMountaineerClimbs(string mountaineerId, string hikeId)
        {
            _module.AddMountaineerClimbs(new Guid(hikeId), new Guid(mountaineerId));
            return Ok();
        }*/





    }
}