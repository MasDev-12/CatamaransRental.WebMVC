using CatamaransRental.Domain;
using CatamaransRental.Domain.Models;
using CatamaransRental.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.Services.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<IEnumerable<UserViewModel>>> GetUsers();
        Task<IBaseResponse<User>> GetUserById(int id);
        Task<IBaseResponse<bool>> DeleteUser(int id);
        Task<IBaseResponse<User>> GetUserByName(string name);
    }
}
