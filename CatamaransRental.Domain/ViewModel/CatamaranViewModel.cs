using CatamaransRental.Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.Domain.ViewModel
{
    public class CatamaranViewModel
    {
        [ValidateNever]
        public long Id { get; set; }
        [ValidateNever]
        public string Model { get; set; }
        [ValidateNever]
        public int Capacity { get; set; }
        [ValidateNever]
        public decimal PricePerHour { get; set; }
        [ValidateNever]
        public bool IsAvailable { get; set; }
        [ValidateNever]
        public ICollection<RentalViewModel> Rentals { get; set; }
        [ValidateNever]
        public string? Image { get; set; }
    }
}
