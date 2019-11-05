using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IEnumerable<string> Get()
        {
            // List of all accounts

            // The header will have the authorization token, and the webid
            return new string[] { "value1", "value2" };
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
