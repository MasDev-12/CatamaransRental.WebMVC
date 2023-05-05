using CatamaransRental.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.Domain.ViewModel
{
    public class PurchaseAttemptViewModel
    {
        public int CatamaranId { get; set; }
        public CatamaranViewModel CatamaranVIewModel { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool IsSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
