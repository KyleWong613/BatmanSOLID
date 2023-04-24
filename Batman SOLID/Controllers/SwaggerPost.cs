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
    public class SwaggerPostController : ControllerBase
    {


        private readonly ILogger<SwaggerPostController> _logger;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public SwaggerPost[] swagpost { get; private set; }

        public SwaggerPostController(ILogger<SwaggerPostController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "PostSwagger")]
        public IEnumerable<SwaggerPost> Post()
        {
            swagpost = Enumerable.Range(1, 5).Select(index => new SwaggerPost
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            return swagpost;
        }


        //now let's see a class that is definitely not following the Single-responsibility principle
        //Batman should only fighting bad guys. and bad guys only.
        //He should not be doing other things.

        public class BatmanBreakingSController
        {
            public void BeatBadGuys()
            {
                try
                {
                    StartBattleWithBadGuy();
                }
                catch (Exception ex)
                {
                    //Here Batman is tryign to find other people, calling them,
                    //waiting for them
                    //doing so many things. not cool at all.
                    Console.WriteLine("Find out Other Members Of Justice League");
                    Console.WriteLine("Find out who amongst these super heroes can fight the villain");
                    Console.WriteLine("Wait for them to takeover the villain battle");
                    Console.WriteLine(ex.ToString());
                }
            }

            private void StartBattleWithBadGuy()
            {
                Console.WriteLine("Battle with Bad Guy Started");
            }
        }

        //Here is Batman again, making proper usage of S in SOLID Principles
        //Batman focuses on fighting bad guys. Just like before.
        public class BatmanBeingCool
        {
            JusticeLeagueEmergencySignal justiceLeagueEmergencySignal = new JusticeLeagueEmergencySignal();
            public void BeatBadGuys()
            {
                try
                {
                    StartBattleWithBadGuy();
                }
                catch (Exception ex)
                {
                    //Batman no longer has to do the many steps required to call for help
                    //Also, Batman just sends signal for help
                    //He need worry how the signal sending works and who will process it 
                    //and so on and so forth.
                    justiceLeagueEmergencySignal.SendSignalForHelp(ex);
                }
            }

            private void StartBattleWithBadGuy()
            {
                Console.WriteLine("Battle with Bad Guy Started");
            }
        }

        public class JusticeLeagueEmergencySignal
        {
            //Batman signal reaches this function and it takes care of everything
            //Also, other super heroes can use it too.
            internal void SendSignalForHelp(Exception ex)
            {
                Console.WriteLine("Find out Other Members Of Justice League");
                Console.WriteLine("Find out who amongst these super heroes can fight the villain");
                Console.WriteLine("Wait for them to takeover the villain battle");
                Console.WriteLine(ex.ToString());
            }
        }

    }

}