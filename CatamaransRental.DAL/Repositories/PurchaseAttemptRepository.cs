using CatamaransRental.DAL.Interfaces;
using CatamaransRental.Domain.Models;
using CatamaransRental.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.DAL.Repositories
{
    public class PurchaseAttemptRepository:IBaseRepository<PurchaseAttempt>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PurchaseAttemptRepository(ApplicationDbContext applicationDbContext) 
        {
            _applicationDbContext=applicationDbContext;
        }

        public async Task<bool> Create(PurchaseAttempt entity)
        {
            await _applicationDbContext.PurchaseAttempts.AddAsync(entity);
            return SaveChangeInDb(await _applicationDbContext.SaveChangesAsync());
        }

        public async Task<bool> Delete(PurchaseAttempt entity)
        {
            _applicationDbContext.PurchaseAttempts.Remove(entity);
            return SaveChangeInDb(await _applicationDbContext.SaveChangesAsync());
        }

        public IQueryable<PurchaseAttempt> GetAll()
        {
            return _applicationDbContext.PurchaseAttempts;
        }

        public async Task<PurchaseAttempt> Update(PurchaseAttempt entity)
        {

            _applicationDbContext.PurchaseAttempts.Update(entity);
            await _applicationDbContext.SaveChangesAsync();
            return entity;
        }
        bool SaveChangeInDb(int save)
        {
            return save>0 ? true : false;
        }
    }
}
