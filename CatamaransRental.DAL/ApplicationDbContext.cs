using CatamaransRental.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.DAL
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();//конструктор нужен для того, чтобы зарегать в asp.net core приложении класс, чтобы установить связь с базой данных,
            //вся настройка подключения будет находится в классе startup
            //EnsureCreated - создается база данных, если она не была создана
            //EnsureDelete - удаленние база данных
        }
        public DbSet<User> Users { get; set; } 
        public DbSet<Catamaran> Catamarans { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseAttempt> PurchaseAttempts { get; set; }
    }
}
