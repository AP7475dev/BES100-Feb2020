using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LibraryApi.Controllers
{
    public class StatusController : Controller
    {
        [HttpGet("status")]
        public ActionResult<StatusResponse> GetTheStatus()
        {
            var response = new StatusResponse
            {
                Status = "Looks good up here, captain!",
                CreatedAt = DateTime.Now
            };
            return Ok(response);
        }


        [HttpGet("employees/{employeeId:int:min(1)}/salary")]
        public ActionResult GetEmployeeSalary(int employeeId)
        {
            return Ok($"Employee {employeeId} hash  a salalry of $65,000");
        }

        [HttpGet("shoes")]
        public ActionResult GetSomeShoes([FromQuery] string color = "all")
        {
            return Ok($"Getting you shoes of color {color}");
        }
        // localhost:1337/shoes?color=blue


        [HttpGet("whoami")]
        public ActionResult WhoAmI([FromHeader(Name = "User-Agent")] string userAgent)
        {
            return Ok($"You are using {userAgent}");
        }

        [HttpPost("employees")]
        public ActionResult AddAnEmployee([FromBody] NewEmployee employee, [FromServices] IGeneratorEmpolyeeIds idGenerator)
        {
            // var idGenerator = new EmpolyeeIdGenerator();
            var id = idGenerator.GetNewEmployeeId();
            return Ok($"Hiring {employee.Name} starting at {employee.StartingSalary.ToString("c")} with an id of {id.ToString()}");
        }

        public class NewEmployee
        {
            public string Name { get; set; }
            public decimal StartingSalary { get; set; }
        }
        
        // localhost:1337/employees - put employee data in body
        /*
         * {
	        "name": "Bob Smith",
	        "startingSalary": 52000
            }
         */

    }

    public class StatusResponse
    {
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
