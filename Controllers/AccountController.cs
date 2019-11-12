using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleSolidPrototype.Agents;

namespace SimpleSolidPrototype.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public string AuthorizationToken => Request.Headers["Authorization"];

        public string WebId => Request.Headers["WebId"];

        // GET: api/Account
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var solidAgent = new SolidAgent(AuthorizationToken, WebId);
            var content = await solidAgent.GetPrivateFolderTurtle();

            return new List<string>() { "hello", "world", content };
        }

        // GET: api/Account/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Account
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Account/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
