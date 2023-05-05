using CatamaransRental.DAL.Interfaces;
using CatamaransRental.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.DAL.Repositories
{
    public class PurchaseRepository:IBaseRepository<Purchase>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PurchaseRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext=applicationDbContext;
        }

        public async Task<bool> Create(Purchase entity)
        {
            await _applicationDbContext.Purchases.AddAsync(entity);
            return SaveChangeInDb(await _applicationDbContext.SaveChangesAsync());
        }

        public async Task<bool> Delete(Purchase entity)
        {
            _applicationDbContext.Purchases.Remove(entity);
            return SaveChangeInDb(await _applicationDbContext.SaveChangesAsync());
        }

        public IQueryable<Purchase> GetAll()
        {
            return _applicationDbContext.Purchases;
        }

        public async Task<Purchase> Update(Purchase entity)
        {

            _applicationDbContext.Purchases.Update(entity);
            await _applicationDbContext.SaveChangesAsync();
            return entity;
        }

        bool SaveChangeInDb(int save)
        {
            return save>0 ? true : false;
        }
    }
}
