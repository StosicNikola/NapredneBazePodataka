using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using naprednebazeback.ObjectModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neo4jClient;
using System.Text;

namespace naprednebazeback.Modules
{
    public class AccountController:ControllerBase
    {
        private static IGraphClient _graphClient;
        private static ILogger<AccountController> _logger;
        public AccountController(IGraphClient graphClient,ILogger<AccountController> logger)
        {
            _graphClient = graphClient;
            _logger = logger;
        }
        
        [HttpGet]
        [Route("SignIn/{email}/{password}")]
        public async Task<ActionResult> SignIn(string email, string password)
        {
           
    
            var obj = await _graphClient.Cypher.Match("(a:Account)<-[h:hasAccount]-(p:Person)")
                                        .Where((Account a)=> a.email == email && a.password == password)
                                        .With("p{ .*, Id:id(p), role:h.role, accountId: id(a)} AS person")
                                        .Return(( person)=>new  {
                                            person = person.As<Person>()
                                            }
                                        ).ResultsAsync;
               
                return Ok(obj);
        }
        [HttpPost]
        [Route("SignUp/Mountaineer/{email}/{password}")]
        public async Task<ActionResult> SignUp(string email, string password)
        {
           
            Dictionary<string, object> dictParam = new Dictionary<string, object>();
            dictParam.Add("Email", email);
            dictParam.Add("Password", password);
            var obj = await _graphClient.Cypher.Create("(a:Account{email:$Email, password: $Password})")
                                                .WithParams(dictParam)
                                                .With("a{.*, Id:id(a)}")
                                                .Return(a=>a.As<Account>()).ResultsAsync;
            if(obj.Count()!=0)
            {
                return Ok(obj);
            }
            else
                return BadRequest("Error creating account");
        }
        [HttpPost]
        [Route("admin/{email}/{password}")]
        public async Task<ActionResult> SignUpAdmin(string email, string password)
        {
                Dictionary<string, object> dictParam = new Dictionary<string, object>();
                dictParam.Add("Email", email);
                dictParam.Add("Password", password);

               var obj =  await _graphClient.Cypher.Create("(a:Account{email:$Email, password: $Password})")
                                            .WithParams(dictParam)
                                            .With("a{.*, Id:id(a)}")
                                                .Return(a=>a.As<Account>())
                                                .ResultsAsync;
            if(obj.Count()!=0)
            {
                return Ok(obj);
            }
            else
                return BadRequest("Error creating account");
        }
        [HttpDelete]
        [Route("{personId}/{accountId}")]
        public async Task<ActionResult> DeleteAccount(long personId, long accountId)
        {
            await _graphClient.Cypher.Match("(a:Account)<-[h:hasAccount]-(p:Person)")
                                                .Where("id(a)=$accountId and id(p) = $id")
                                                .WithParam("accountId",accountId)
                                                .WithParam("id",personId)
                                                .DetachDelete("a")
                                                .Delete("p")
                                                .ExecuteWithoutResultsAsync();
            return Ok();
        }

    }
}