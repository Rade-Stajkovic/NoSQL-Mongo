using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using NBP___Mongo.Model;
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

        public async Task<IActionResult> CreateUser( string username, string password, string name, string location)
        {
            try
            {
                var result = await dealerService.CreateDealer( username, password, name, location);
                if (result == "Uspešno kreiran korisnik.")
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




        [HttpGet]
        [Route("LogInDealer/{username}/{password}")]
        public IActionResult LogInDealer(String username, String password)
        {
            try
            {
                Task<Dealer> res = dealerService.LogInDealer(username, password);
                Dealer res1 = res.Result;
                if (res1 == null)
                {
                    return BadRequest("Korisnik ne postoji ili ste pogresli parametre za prijavu");
                }

                return Ok(res1);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
