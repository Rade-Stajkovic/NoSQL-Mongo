using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NBP___Mongo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBP___Mongo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly CarService carService;

        public CarController(CarService carService)
        {
            this.carService = carService;
        }

        [HttpPost]

        public IActionResult AddCar(String description, String year, String interiorColor, String exteriorColor)
        {
            try
            {
                carService.AddNewCarAsync(description, year, interiorColor, exteriorColor);
                return Ok("uspelo");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
