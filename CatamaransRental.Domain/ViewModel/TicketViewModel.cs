using CatamaransRental.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.Domain.ViewModel
{
    public class TicketViewModel
    {
        public int CatamaranId { get; set; }
        public CatamaranViewModel CatamaranViewModel { get; set; }
        public int RentalId { get; set; }
        public RentalViewModel Rental { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int DurationInHours { get; set; }
        public decimal TotalCost { get; set; }

        public bool IsUsed { get; set; }
    }
}
