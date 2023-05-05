using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.Domain.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalCost { get; set; }
        public int CatamaranId { get; set; }
        public Catamaran Catamaran { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
