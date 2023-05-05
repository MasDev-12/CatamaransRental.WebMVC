﻿using CatamaransRental.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.Domain.ViewModel
{
    public class PurchaseViewModel
    {
        //public int? TicketId { get; set; }
        //public Ticket Ticket { get; set; }
        public int TicketId { get; set; }
        public TicketViewModel TicketViewModel { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
    }
}
