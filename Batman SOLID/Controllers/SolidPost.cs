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

                    var devCalculations = new List<BaseSalaryCalculator>
                    {
                        new JuniorDevSalaryCalculator(new DeveloperReport {Id = 1, Name = "Dev1", Level = "Junior developer", HourlyRate = 20, WorkingHours = 100 }),
                        new SeniorDevSalaryCalculator(new DeveloperReport {Id = 2, Name = "Dev2", Level = "Senior developer", HourlyRate = 30.5, WorkingHours = 120 }),
                        new SeniorDevSalaryCalculator(new DeveloperReport {Id = 3, Name = "Dev3", Level = "Senior developer", HourlyRate = 30.5, WorkingHours = 150 })
                    };
                    var calculator = new SalaryCalculator(devCalculations);

                    solidpost = "Sum of all the developer salaries: " + calculator.CalculateTotalSalaries();
                    break;

                case "l":
                    solid = "Liskov substitution principle";
                    string apple = "";
                    string orange = "";
                    IFruit fruit = new Orange();
                    orange = "Color of Orange: " + fruit.GetColor();
                    fruit = new Apple();
                    apple = "Color of Apple: " + fruit.GetColor();

                    Console.ReadKey();
                    solidpost = apple + orange;
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

        //Single Responsibility
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

        //Open Closed
        public abstract class BaseSalaryCalculator
        {
            protected DeveloperReport DeveloperReport { get; private set; }
            public BaseSalaryCalculator(DeveloperReport developerReport)
            {
                DeveloperReport = developerReport;
            }
            public abstract double CalculateSalary();
        }
        public class SalaryCalculator
        {
            private readonly IEnumerable<BaseSalaryCalculator> _developerCalculation;
            public SalaryCalculator(IEnumerable<BaseSalaryCalculator> developerCalculation)
            {
                _developerCalculation = developerCalculation;
            }
            public double CalculateTotalSalaries()
            {
                double totalSalaries = 0D;
                foreach (var devCalc in _developerCalculation)
                {
                    totalSalaries += devCalc.CalculateSalary();
                }
                return totalSalaries;
            }
        }
        public class JuniorDevSalaryCalculator : BaseSalaryCalculator
        {
            public JuniorDevSalaryCalculator(DeveloperReport developerReport)
                : base(developerReport)
            {
            }
            public override double CalculateSalary() => DeveloperReport.HourlyRate * DeveloperReport.WorkingHours;
        }
        public class SeniorDevSalaryCalculator : BaseSalaryCalculator
        {
            public SeniorDevSalaryCalculator(DeveloperReport report)
                : base(report)
            {
            }
            public override double CalculateSalary() => DeveloperReport.HourlyRate * DeveloperReport.WorkingHours * 1.2;
        }

        //Liskov substitution

        public interface IFruit
        {
            string GetColor();
        }
        public class Apple : IFruit
        {
            public string GetColor()
            {
                return "Red";
            }
        }
        public class Orange : IFruit
        {
            public string GetColor()
            {
                return "Orange";
            }
        }
    }

}