using CatamaransRental.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.Domain.Models
{
    public class PurchaseAttempt
    {
        public int Id { get; set; }
        //public int? TicketId { get; set; }
        //public Ticket Ticket { get; set; }
        public int? CatamaranId { get; set; }
        public Catamaran Catamaran { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool IsSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
