using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//The single-responsibility principle (SRP) is a computer - programming principle that states that every module, class or function in a computer program should have responsibility over a single part of that program's functionality, and it should encapsulate that part. All of that module, class or function's services should be narrowly aligned with that responsibility
//https://en.wikipedia.org/wiki/Single-responsibility_principle

//The idea is, each class should do one specific thing, and one thing only.
//For example, Batman should focus on fighting and fighting only, especially when he has encountered a villain
//In the middle of a battle Batman should not be (Or, he may not have the time), messing around with sorting algorithms and phone calls to find out which super hero is available to help.

namespace Batman_SOLID.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SolidPostController : ControllerBase
    {
        private readonly ILogger<SolidPostController> _logger;
        public SolidPost[] solidpost { get; private set; }
        public SolidPostController(ILogger<SolidPostController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "PostSolid")]
        [Produces("application/json")]
        [Consumes("application/json")] 
        public IActionResult Post([FromBody] SolidPost inputPara)
        {
            string solidtype = inputPara.type.ToLower();
            string solid = "";
            string solidpost = "";
            int i = 1;
            switch (solidtype)
            {
                case "s":
                    solid = "single responsibility principle";
                    string res = "Responsibility "; 
                    solidpost = res + "1: " + StudentRegister() + " " + 
                                res + "2: " + StudentEmails() + " " + 
                                res + "3: " + StudentResult();

                    break;
                case "o":
                    solid = "open-closed principle";
                    break;
                case "l":
                    solid = "Liskov substitution principle";
                    break;
                case "i":
                    solid = "interface segregation principle";
                    break;
                case "d":
                    solid = "dependency inversion principle";
                    break;
                default:
                    break;
            } 
            return Ok(new {Principle = solid, SOLID = solidpost });
        }

        private string StudentRegister()
        {
            return "Student Registered";
        }
         
        private string StudentResult()
        { 
            return "Student Result Calculated";
        }
        private string StudentEmails()
        { 
            return "Send Email to Student";
            
        } 
    }

}