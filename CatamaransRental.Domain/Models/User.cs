using CatamaransRental.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Password { get; set; }//123456->89sadas98sadsa
        public string Name { get; set; }
        //public string Email { get; set; }
        public Role Role { get; set; }
    }
}
