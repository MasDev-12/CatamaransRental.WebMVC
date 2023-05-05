using AutoMapper;
using CatamaransRental.DAL.Interfaces;
using CatamaransRental.DAL.Repositories;
using CatamaransRental.Domain.Enum;
using CatamaransRental.Domain.Models;
using CatamaransRental.Domain.Response;
using CatamaransRental.Domain.ViewModel;
using CatamaransRental.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.Services.Implementions
{
    public class CatamaranService : ICatamaranService
    {
        private readonly IBaseRepository<Catamaran> _catamaranRepository;
        private readonly IMapper _mapper;

        public CatamaranService(IBaseRepository<Catamaran> catamaranRepository,IMapper mapper) 
        {
            _catamaranRepository=catamaranRepository;
            _mapper=mapper;
        }
        public IBaseResponse<IEnumerable<CatamaranViewModel>> GetAllCatamarans()
        {
            try
            {
                var catamarans = _catamaranRepository.GetAll().AsNoTracking().ToList();
                var catamaransViewModels = _mapper.Map<IEnumerable<CatamaranViewModel>>(catamarans);
                if (catamarans.Count==0)
                {
                    return new BaseResponse<IEnumerable<CatamaranViewModel>>()
                    {
                        StatusCode=Domain.Enum.StatusCodeEnum.NotFound,
                        Description="Найдено ноль элементов"
                    };
                };
                return new BaseResponse<IEnumerable<CatamaranViewModel>>()
                {
                    Data= catamaransViewModels,
                    StatusCode=Domain.Enum.StatusCodeEnum.OK,
                    Description="Найденые модели"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<CatamaranViewModel>>()
                {
                    Description= $"[GetAllCatamarans]: {ex.Message}",
                    StatusCode = StatusCodeEnum.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<CatamaranViewModel>> GetCatamaranByModel(string model)
        {
            try
            {
                var catamaran = await _catamaranRepository.GetAll().FirstOrDefaultAsync(x => x.Model==model);
                if(catamaran == null)
                {
                    return new BaseResponse<CatamaranViewModel>()
                    {
                        StatusCode=Domain.Enum.StatusCodeEnum.NotFound,
                        Description="Найдено ноль элементов"
                    };
                }
                var catamaranViewModel = _mapper.Map<CatamaranViewModel>(catamaran);
                return new BaseResponse<CatamaranViewModel>()
                {
                    Data = catamaranViewModel,
                    StatusCode=Domain.Enum.StatusCodeEnum.OK,
                    Description="Найден один элемент"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CatamaranViewModel>()
                {
                    Description= $"[GetCatamaranById]: {ex.Message}",
                    StatusCode = StatusCodeEnum.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> Update(int id)
        {
            try
            {
               
                var catamaran = _catamaranRepository.GetAll().FirstOrDefault(c => c.Id==id);
                if (catamaran==null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data= false,
                        Description="Найдено ноль элементов",
                        StatusCode=Domain.Enum.StatusCodeEnum.NotFound
                    };
                }
                //catamaran.IsAvailable=false;
                catamaran.IsAvailable=false;
                await _catamaranRepository.Update(catamaran);
                return new BaseResponse<bool>()
                {
                    Data= true,
                    Description="Обновление прошло успешно",
                    StatusCode=Domain.Enum.StatusCodeEnum.OK
                };
            }

            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description= $"[Update]: {ex.Message}",
                    StatusCode = StatusCodeEnum.InternalServerError
                };
            }
        }
    }
}
