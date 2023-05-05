using CatamaransRental.DAL.Interfaces;
using CatamaransRental.Domain.Enum;
using CatamaransRental.Domain.Helpers;
using CatamaransRental.Domain.Models;
using CatamaransRental.Domain.Response;
using CatamaransRental.Domain.ViewModel;
using CatamaransRental.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.Services.Implementions
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<User> _userRepository;

        public AccountService(IBaseRepository<User> userRepository)
        {
            this._userRepository=userRepository;
        }
        public async Task<BaseResponse<ClaimsIdentity>> LoginUser(LoginViewModel loginViewModel)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.Name==loginViewModel.Name);
                if (user==null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description="Пользователь не найден",
                    };
                }
                if (user.Password!=loginViewModel.Password)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description="Неверный пароль",
                    };
                }
                var result = Authenticate(user);
                return new BaseResponse<ClaimsIdentity>()
                {
                    Data= result,
                    StatusCode=StatusCodeEnum.OK
                };
            }
            catch (Exception ex)
            {

                return new BaseResponse<ClaimsIdentity>()
                {
                    Description= $"[LoginUser]: {ex.Message}",
                    StatusCode = StatusCodeEnum.InternalServerError
                };
            }
        }


        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
