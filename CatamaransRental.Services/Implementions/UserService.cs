using CatamaransRental.DAL.Interfaces;
using CatamaransRental.Domain;
using CatamaransRental.Domain.Enum;
using CatamaransRental.Domain.Extension;
using CatamaransRental.Domain.Models;
using CatamaransRental.Domain.Response;
using CatamaransRental.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.Services.Implementions
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;
        public UserService(IBaseRepository<User> userRepository)
        {
            _userRepository=userRepository;
        }
        public async Task<IBaseResponse<bool>> DeleteUser(int id)
        {

            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id==id);
                if (user==null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description="Найдено ноль элементов",
                        StatusCode=Domain.Enum.StatusCodeEnum.NotFound,
                        Data=false,
                    };
                }
                await _userRepository.Delete(user);
                return new BaseResponse<bool>()
                {
                    Data=true,
                    Description="Пользователь найден и удален",
                    StatusCode=StatusCodeEnum.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description= $"[DeleteUser]: {ex.Message}",
                    StatusCode = StatusCodeEnum.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<User>> GetUserById(int id)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.Id==id);
                if (user==null)
                {
                    return new BaseResponse<User>()
                    {
                        Description="Найдено ноль элементов",
                        StatusCode=Domain.Enum.StatusCodeEnum.NotFound,
                    };
                }
                return new BaseResponse<User>()
                {
                    Data=user,
                    Description="Найдено успешно",
                    StatusCode = StatusCodeEnum.OK
                };
            }
            catch (Exception ex)
            {

                return new BaseResponse<User>()
                {
                    Description= $"[GetUserById]: {ex.Message}",
                    StatusCode = StatusCodeEnum.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<User>> GetUserByName(string name)
        {
            try
            {
                var user = _userRepository.GetAll().FirstOrDefaultAsync(u => u.Name==name).Result;
                if (user==null)
                {
                    return new BaseResponse<User>()
                    {
                        Description="Найдено ноль элементов",
                        StatusCode=Domain.Enum.StatusCodeEnum.NotFound,
                    };
                }
                return new BaseResponse<User>()
                {
                    Data=user,
                    Description="Найдено успешно",
                    StatusCode = StatusCodeEnum.OK
                };
            }
            catch (Exception ex)
            {

                return new BaseResponse<User>()
                {
                    Description= $"[GetUserById]: {ex.Message}",
                    StatusCode = StatusCodeEnum.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<UserViewModel>>> GetUsers()
        {
            try
            {
                var users = _userRepository.GetAll()
                    .Select(x => new UserViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Role=x.Role.GetDisplayName(),
                    });

                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Data= users,
                    StatusCode=Domain.Enum.StatusCodeEnum.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {

                    StatusCode=Domain.Enum.StatusCodeEnum.InternalServerError,
                    Description=$"Ошибка подключения + {ex.Message}"

                };
            }
        }
    }
}
