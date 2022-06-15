using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReactExcelFile.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactExcelFile.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private string _connectionString;

        public PeopleController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }


        [HttpGet]
        [Route("getpeople")]
        public List<Person> GetPeople()
        {
            var repo = new PeopleRepo(_connectionString);
            return repo.GetPeople();
        }

        [HttpPost]
        [Route("delete")]
        public void Delete()
        {
            var repo = new PeopleRepo(_connectionString);
            repo.Delete();
        }

        [HttpPost]
        [Route("upload")]
        public void Upload(FileViewModel viewModel)
        {
            int index = viewModel.Base64.IndexOf(",") + 1;
            string base64 = viewModel.Base64.Substring(index);
            byte[] imageBytes = Convert.FromBase64String(base64);
            var people = GetfromCsvBytes(imageBytes);
            var repo = new PeopleRepo(_connectionString);
            repo.AddPeople(people);
        }
        static List<Person> GetfromCsvBytes(byte[] csvBytes)
        {
            using var memoryStream = new MemoryStream(csvBytes);
            var streamReader = new StreamReader(memoryStream);
            using var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            return reader.GetRecords<Person>().ToList();
        }


        [HttpGet]
        [Route("generate")]
        public IActionResult Generate(int count)
        {
            var people = GetPeople(count);
            string csv = GetCsv(people);
            byte[] bytes = Encoding.UTF8.GetBytes(csv);
            return File(bytes, "text/csv", "people.csv");
        }

        public List<Person> GetPeople(int count)
        {
            List<Person> result = new();
            for (int i = 1; i <= count; i++)
            {
                result.Add(new Person
                {
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Address = Faker.Address.StreetAddress(),
                    Email = Faker.Internet.Email(),
                    Age = Faker.RandomNumber.Next(20, 100)
                });
            }

            return result;
        }
        private string GetCsv(List<Person> people)
        {
            var builder = new StringBuilder();
            var stringWriter = new StringWriter(builder);
            using var csv = new CsvWriter(stringWriter, CultureInfo.InvariantCulture);
            csv.WriteRecords(people);
            return builder.ToString();
        }




    }
}
