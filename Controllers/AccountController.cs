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
        public string AccessToken => Request.Headers["Authorization"];

        public string WebId
        {
            get
            {
                var webIdHeader = Request.Headers["WebId"];

                if (webIdHeader.Count > 0)
                {
                    return webIdHeader.ToString().Replace("/profile/card#me", string.Empty);
                }
                
                return string.Empty;
            }
        }

        // GET: api/Account
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var solidAgent = new SolidAgent(AccessToken, WebId);
            var content = await solidAgent.GetAccounts();

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
