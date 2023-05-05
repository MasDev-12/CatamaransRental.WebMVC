using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.Domain.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int CatamaranId { get; set; }
        public Catamaran Catamaran { get; set; }
        public int RentalId { get; set; }
        public Rental Rental { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int DurationInHours { get; set; }
        public decimal TotalCost { get; set; }

        public bool IsUsed { get; set; }
    }
}
