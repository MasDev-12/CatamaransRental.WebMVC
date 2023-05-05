using CatamaransRental.Domain.Response;
using CatamaransRental.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.Services.Interfaces
{
    public interface IAccountService
    {
        //Task<BaseResponse<ClaimsIdentity>> RegisterUser(RegisterViewModel registerViewModel);
        //Task<BaseResponse<ClaimsIdentity>> RegisterAdmin(RegisterViewModel registerViewModel);
        Task<BaseResponse<ClaimsIdentity>> LoginUser(LoginViewModel loginViewModel);
    }
}
