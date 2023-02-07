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
        [Route("AddCar")]
        public async Task<IActionResult> AddCar(String description, String year, String interiorColor, String exteriorColor)
        {
            try
            {
                await carService.AddNewCarAsync(description, year, interiorColor, exteriorColor);
                return Ok("Uspesno dodat automobil");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteCar")]
        public async Task<IActionResult> DeleteCar(String id)
        {
            try
            {
                bool rez = await carService.DeleteCar(id);
                if (rez)
                {
                    return Ok("Uspesno obrisan automobil");
                }
                return BadRequest("Automobil nije obrisan");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("AddMark")]
        public async Task<IActionResult> AddMark(String name, String origin)
        {
            try
            {
                await carService.AddNewMark(name, origin);
                return Ok("Uspesno dodata marka");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("AddModel")]
        public async Task<IActionResult> AddModel(String name)
        {
            try
            {
                await carService.AddNewModel(name);
                return Ok("Uspesno dodat model");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("AddModelToMark/{nameMark}/{nameModel}")]
        public async Task<IActionResult> AddModelToMark(String nameMark, String nameModel)
        {
            try
            {
                var rez = await carService.AddModelToMark(nameMark, nameModel);
                if (rez)
                {
                    return Ok("Uspesno dodat model u listu");
                }
                return BadRequest("Greska");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
