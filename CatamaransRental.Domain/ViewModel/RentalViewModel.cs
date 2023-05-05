using CatamaransRental.Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.Domain.ViewModel
{
    public class RentalViewModel
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [BindNever]
        public decimal? TotalCost { get; set; }
        [BindNever]
        public int? CatamaranId { get; set; }
        [BindNever]
        public CatamaranViewModel? CatamaranViewModel { get; set; } = new CatamaranViewModel();
        [BindNever]
        public int? UserId { get; set; }
        [BindNever]
        public UserViewModel? UserViewModel { get; set; } = new UserViewModel();
    }
}
