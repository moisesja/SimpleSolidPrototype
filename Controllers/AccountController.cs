using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using SimpleSolidPrototype.Agents;

namespace SimpleSolidPrototype.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public string AccessToken => Request.Headers["Authorization"];

        public string WebId => Request.Headers["WebId"];

        // GET: api/Account
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            try
            {
                var solidAgent = new SolidAgent(AccessToken, WebId, "https://localhost:5001");
                var content = await solidAgent.GetAccounts();

                RequestHeaders header = Request.GetTypedHeaders();
                Uri uriReferer = header.Referer;



                return new List<string>() { "hello", "world", content };
            }
            catch (Exception exc)
            {
                throw;
            }
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
