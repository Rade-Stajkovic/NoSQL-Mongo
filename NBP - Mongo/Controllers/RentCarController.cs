using Microsoft.AspNetCore.Mvc;
using MongoDbContext;
using NBP___Mongo.Model;
using NBP___Mongo.Services;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NBP___Mongo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RentCarController : ControllerBase
    {
        private readonly RentCarService _rentCarService;
        public RentCarController(RentCarService service)
        {
            this._rentCarService = service;
        }
      
        [HttpGet]
        [Route("GetDealersRentals/{DealerID}")]
        public async Task<IActionResult> GetDealersRentals(string DealerID)
        {
            List<RentCar> list = await _rentCarService.GetDealersRentals(DealerID);
            return new JsonResult(list);
        }

        [HttpPost]
        [Route("MakeCarRental/{OccupiedFrom}/{OccupiedUntill}/{CarID}/{DealerID}/{UserID}")]
        public async Task<IActionResult> MakeCarRental(DateTime OccupiedFrom, DateTime OccupiedUntill, string CarID, string DealerID, string UserID)
        {
            bool status = await _rentCarService.MakeCarRental(OccupiedFrom, OccupiedUntill, CarID, DealerID, UserID);
            if (status) return Ok("Succesfully created rental!");
            return BadRequest("Error");
        }

        [HttpPut]
        [Route("AllowRental/{RentalID}")]
        public async Task<IActionResult> AllowRental(string RentalID)
        {
            bool status = await _rentCarService.AllowRental(RentalID);
            if (status) return Ok("Succesfully allowed rental!");
            return BadRequest("Error");
        }

        [HttpPut]
        [Route("FrobidRental/{RentalID}")]
        public async Task<IActionResult> FrobidRental(string RentalID)
        {
            bool status = await _rentCarService.FrobidRental(RentalID);
            if (status) return Ok("Succesfully allowed rental!");
            return BadRequest("Error");
        }


    }
}
