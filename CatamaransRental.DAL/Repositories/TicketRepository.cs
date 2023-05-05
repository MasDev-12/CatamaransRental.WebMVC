using CatamaransRental.DAL.Interfaces;
using CatamaransRental.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.DAL.Repositories
{
    public class TicketRepository : IBaseRepository<Ticket>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TicketRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext=applicationDbContext;
        }
        public async Task<bool> Create(Ticket entity)
        {
            await _applicationDbContext.Tickets.AddAsync(entity);
            return SaveChangeInDb(await _applicationDbContext.SaveChangesAsync());
        }

        public async Task<bool> Delete(Ticket entity)
        {
            _applicationDbContext.Tickets.Remove(entity);
            return SaveChangeInDb(await _applicationDbContext.SaveChangesAsync());
        }

        public IQueryable<Ticket> GetAll()
        {
            return _applicationDbContext.Tickets;
        }

        public async Task<Ticket> Update(Ticket entity)
        {
            _applicationDbContext.Set<Ticket>().AsNoTracking();
            _applicationDbContext.Tickets.Update(entity);
            await _applicationDbContext.SaveChangesAsync();
            return entity;
        }

        bool SaveChangeInDb(int save)
        {
            return save>0 ? true : false;
        }

    }
}
