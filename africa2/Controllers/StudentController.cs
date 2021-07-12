using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using africa2.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace africa2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private static readonly string[] StudentFirstNames = new[]
        {
            "Billy", "Bobby", "Ricky", "Sam", "Joe", "Sarah"
        };

        private static readonly string[] StudentLastNames = new[]
        {
            "Mac", "Smith", "Jones", "Gills", "Rock", "Goliath"
        };

        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Student> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Student
            {
                FirstName = StudentFirstNames[rng.Next(StudentFirstNames.Length)],
                LastName = StudentLastNames[rng.Next(StudentLastNames.Length)],
            })
            .ToArray();
        }
    }
}
