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
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;

        }

        public IActionResult CreateUser(string name, string surname, string username, string password)
        {
            try
            {
                userService.CreateUser(name, surname, username, password);
                return Ok("uspelo");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
