using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NBP___Mongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentCarController : ControllerBase
    {
        // GET: api/<RentCarController>
        [HttpGet]
        public string Get()
        {
            return "value1";
        }

        // GET api/<RentCarController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RentCarController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RentCarController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RentCarController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
