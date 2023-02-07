using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using NBP___Mongo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBP___Mongo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DealerController : ControllerBase
    {
        private readonly DealerService dealerService;

        public DealerController(DealerService dealerService)
        {
            this.dealerService = dealerService;
        }


        [HttpPost]

        public IActionResult CreateDealer(string username, string password, string name, string location, IEnumerable<ObjectId> carIds)
        {
            try
            {
                dealerService.CreateDealer(username, password, name ,location,carIds);
                return Ok("uspelo");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
