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
    public class CatamaranRepository:IBaseRepository<Catamaran>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CatamaranRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext=applicationDbContext;
        }

        public async Task<bool> Create(Catamaran entity)
        {
            await _applicationDbContext.Catamarans.AddAsync(entity);
            return SaveChangeInDb(await _applicationDbContext.SaveChangesAsync());
        }

        public async Task<bool> Delete(Catamaran entity)
        {
            _applicationDbContext.Catamarans.Remove(entity);
            return SaveChangeInDb(await _applicationDbContext.SaveChangesAsync());
        }

        public IQueryable<Catamaran> GetAll()
        {
            return _applicationDbContext.Catamarans;
        }

        public async Task<Catamaran> Update(Catamaran entity)
        {
            _applicationDbContext.Set<Catamaran>().AsNoTracking();
            _applicationDbContext.Catamarans.Update(entity);
            await _applicationDbContext.SaveChangesAsync();
            return entity;
        }

        bool SaveChangeInDb(int save)
        {
            return save>0 ? true : false;
        }
    }
}
