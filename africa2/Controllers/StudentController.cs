using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using africa2.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using Npgsql;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace africa2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        // TODO: https://www.postgresql.org/docs/9.1/libpq-ssl.html (SET SSL)
        private readonly IConfiguration _configuration;
        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<Student> Get()
        {
            string query = @"
                SELECT ""FirstName"", ""LastName""
                FROM public.""Student"";
            ";

            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AfricaDb");

            NpgsqlDataReader myReader;

            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, myCon))
                {
                    myReader = cmd.ExecuteReader();

                    dataTable.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }

            }


            // TODO: Figure out how to convert this to JsonResult return


            foreach (DataRow row in dataTable.Rows)
            {
                yield return new Student
                {
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString()
                };
            }

            // end PostgreSQL
        }

        [HttpPost]
        public IEnumerable<Student> Post([FromBody] Student student)
        {
            // Convert to LINQ

            string query = @"
                INSERT INTO public.""Student""(""FirstName"", ""LastName"")
	            VALUES(@FirstName, @LastName);
            ";

            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AfricaDb");

            NpgsqlDataReader myReader;

            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, myCon))
                {
                    cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", student.LastName);

                    myReader = cmd.ExecuteReader();

                    dataTable.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }

            }

            // TODO: Figure out how to convert this to JsonResult return

            return null;
        }

        [HttpPut]
        public IEnumerable<Student> Put([FromBody] Student student)
        {
            // Convert to LINQ

            string query = @"
                UPDATE Student
                set FirstName = @FirstName
                set LastName = @LastName
                WHERE someIdINeedToCreate = @someId
            ";

            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AfricaDb");

            NpgsqlDataReader myReader;

            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, myCon))
                {
                    cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", "Magee");

                    myReader = cmd.ExecuteReader();

                    dataTable.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }

            }

            return null;
        }

        [HttpDelete("id")]
        public IEnumerable<Student> Delete(int id)
        {
            string query = @"
                DELETE FROM Student
                WHERE someIdINeedToCreate = @someId
            ";

            return null;
        }
    }
}