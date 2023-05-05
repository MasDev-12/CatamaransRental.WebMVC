using CatamaransRental.DAL.Interfaces;
using CatamaransRental.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.DAL.Repositories
{
    public class RentalRepository:IBaseRepository<Rental>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RentalRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext=applicationDbContext;
        }

        public async Task<bool> Create(Rental entity)
        {
            await _applicationDbContext.Rentals.AddAsync(entity);
            return SaveChangeInDb(await _applicationDbContext.SaveChangesAsync());
        }

        public async Task<bool> Delete(Rental entity)
        {
            _applicationDbContext.Rentals.Remove(entity);
            return SaveChangeInDb(await _applicationDbContext.SaveChangesAsync());
        }

        public IQueryable<Rental> GetAll()
        {
            return _applicationDbContext.Rentals;
        }

        public async Task<Rental> Update(Rental entity)
        {
            _applicationDbContext.Rentals.Update(entity);
            await _applicationDbContext.SaveChangesAsync();
            return entity;
        }
        bool SaveChangeInDb(int save)
        {
            return save>0 ? true : false;
        }
    }
}
