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
    public class SchoolController : ControllerBase
    {
        // TODO: https://www.postgresql.org/docs/9.1/libpq-ssl.html (SET SSL)
        private readonly IConfiguration _configuration;
        private readonly ILogger<SchoolController> _logger;

        public SchoolController(ILogger<SchoolController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<School> Get()
        {
            string query = @"
                SELECT ""Name"", ""Address""
                FROM public.""School"";
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
                yield return new School
                {
                    Name = row["Name"].ToString(),
                    Address = row["Address"].ToString()
                };
            }

            // end PostgreSQL
        }

        [HttpPost]
        public IEnumerable<School> Post([FromBody] School school)
        {
            // Convert to LINQ

            string query = @"
                INSERT INTO public.""School""(""Name"", ""Address"")
	            VALUES(@Name, @Address);
            ";

            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AfricaDb");

            NpgsqlDataReader myReader;

            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, myCon))
                {
                    cmd.Parameters.AddWithValue("@Name", school.Name);
                    cmd.Parameters.AddWithValue("@Address", school.Address);

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
        public IEnumerable<School> Put([FromBody] School school)
        {
            // Convert to LINQ

            string query = @"
                UPDATE School
                set Name = @Name
                set Address = @Address
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
                    cmd.Parameters.AddWithValue("@Name", school.Name);
                    cmd.Parameters.AddWithValue("@Address", school.Address);

                    myReader = cmd.ExecuteReader();

                    dataTable.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }

            }

            return null;
        }

        [HttpDelete("id")]
        public IEnumerable<School> Delete(int id)
        {
            string query = @"
                DELETE FROM School
                WHERE someIdINeedToCreate = @someId
            ";

            return null;
        }
    }
}