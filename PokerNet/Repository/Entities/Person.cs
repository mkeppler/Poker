using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PokerNet.Repository.Entities
{
    public class Person : EntityBase<Person>, IEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? HostingDate { get; set; }

        public int Order { get; set; }

        [NotMapped]
        public bool IsCurrentPerson { get; set; }
           
    }
}
