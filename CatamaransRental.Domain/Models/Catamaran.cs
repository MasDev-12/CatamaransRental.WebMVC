using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.Domain.Models
{
    public class Catamaran
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerHour { get; set; }
        public bool IsAvailable { get; set; }
        public ICollection<Rental> Rentals { get; set; }
        public string? Image { get; set; }
    }
}
