using PokerNet.Models;
using PokerNet.Repository;
using PokerNet.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace PokerNet.Controllers
{
    
    public class HomeController : ApiController
    {
        private RepositoryContext db = new RepositoryContext();

        //[RoutePrefix("home/api")]
        [Route("api/home/ForDate/{date}")]
        [ResponseType(typeof(string))]
        public string GetForDate(DateTime date)
        {
            var people = db.People.OrderBy(o => o.Order).ToList();

            Person personThisMonth = GetPersonForMonth(people, date);

            return string.Format("{0} {1}", personThisMonth.FirstName, personThisMonth.LastName);
        }
        public HomeResponse GetHome()
        {
            var people = db.People.OrderBy(o => o.Order).ToList();

            Person personThisMonth = GetPersonForMonth(people, DateTime.Now);

            var nextPerson = GetNextPerson(people, personThisMonth);

            return new HomeResponse
            {
                CurrentPerson = string.Format("{0} {1}", personThisMonth.FirstName, personThisMonth.LastName),
                NextPerson = string.Format("{0} {1}", nextPerson.FirstName, nextPerson.LastName)
            };
        }

        private Person GetPersonForMonth(ICollection<Person> people, DateTime date)
        {
            var lastCapturedPerson = people.OrderByDescending(o => o.HostingDate).First();
            var lastCapturedDate = lastCapturedPerson.HostingDate;

            Person personThisMonth = null;
            if (lastCapturedDate.Value.Month == date.Month && lastCapturedDate.Value.Year == date.Year)
                personThisMonth = lastCapturedPerson;
            else
            {
                var numOfPeopleToSkip = ((date.Year - lastCapturedDate.Value.Year) * 12) + date.Month - lastCapturedDate.Value.Month;
                var nextPersonOrder = lastCapturedPerson.Order + numOfPeopleToSkip;
                while (nextPersonOrder > people.Count)
                    nextPersonOrder = nextPersonOrder - people.Count;

                personThisMonth = people.ElementAt(nextPersonOrder - 1);
            }

            return personThisMonth;
        }
        private Person GetNextPerson(ICollection<Person> people, Person currentPerson)
        {
            var personOrder = currentPerson.Order == people.Count ? 0 : currentPerson.Order;

            return people.ElementAt(personOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
