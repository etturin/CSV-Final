using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactExcelFile.Data
{
    public class PeopleRepo
    {
        private readonly string _connectionString;

        public PeopleRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetPeople()
        {
            using var context = new PeopleContext(_connectionString);
            return context.People.ToList();
        }

        public void Delete()
        {
            using var context = new PeopleContext(_connectionString);
            context.People.RemoveRange(context.People);
            context.SaveChanges();
        }
        public void AddPeople(List<Person> people)
        {
            using var context = new PeopleContext(_connectionString);
            people.ForEach(p => context.People.Add(p));
            context.SaveChanges();

        }
    }
}
