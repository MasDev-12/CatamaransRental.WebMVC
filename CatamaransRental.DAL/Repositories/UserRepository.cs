using CatamaransRental.DAL.Interfaces;
using CatamaransRental.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.DAL.Repositories
{
    public class UserRepository : IBaseRepository<User>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext=applicationDbContext;
        }

        public async Task<bool> Create(User entity)
        {
            await _applicationDbContext.Users.AddAsync(entity);
            return SaveChangeInDb(await _applicationDbContext.SaveChangesAsync());
        }

        public async Task<bool> Delete(User entity)
        {
            _applicationDbContext.Users.Remove(entity);
            return SaveChangeInDb(await _applicationDbContext.SaveChangesAsync());
        }

        public  IQueryable<User> GetAll()
        {
            return _applicationDbContext.Users;
        }

        public async Task<User> Update(User entity)
        {
            _applicationDbContext.Users.Update(entity);
            await _applicationDbContext.SaveChangesAsync();
            return entity;
        }
        bool SaveChangeInDb(int save)
        {
            return save>0 ? true : false;
        }
    }
}
