using AutoMapper;
using CatamaransRental.DAL.Interfaces;
using CatamaransRental.Domain.Enum;
using CatamaransRental.Domain.Models;
using CatamaransRental.Domain.Response;
using CatamaransRental.Domain.ViewModel;
using CatamaransRental.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.Services.Implementions
{
    public class RentalService : IRentalService
    {
        private readonly IBaseRepository<Rental> _rentalRepository;
        private readonly IMapper _mapper;
        private readonly ICatamaranService _catamaranService;
        private readonly IBaseRepository<PurchaseAttempt> _purchaseAttempRepository;
        private readonly IPurchaseService _purchaseService;
        private readonly ITicketService _ticketService;
        private readonly IUserService _userService;
        private readonly IBaseRepository<Catamaran> _catamaranRepository;

        public RentalService(IBaseRepository<Rental> rentalRepository,IMapper mapper,ICatamaranService catamaranService,
            IBaseRepository<PurchaseAttempt> purchaseAttempRepository, IPurchaseService purchaseService,
            ITicketService ticketService, IUserService userService,IBaseRepository<Catamaran> catamaranRepository) 
        {
            _mapper=mapper;
            _catamaranService=catamaranService;
            _rentalRepository=rentalRepository;
            _purchaseAttempRepository=purchaseAttempRepository;
            _purchaseService=purchaseService;
            _ticketService=ticketService;
            _userService=userService;
            _catamaranRepository=catamaranRepository;
        }
        public async Task<IBaseResponse<bool>> CreateRental(RentalViewModel rentalViewModel)
        {
            try
            {
                var user = await _userService.GetUserByName(rentalViewModel.UserViewModel.Name);
                if (user.Data==null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description= $"ошибка обратитесь к разработчикам",
                        StatusCode = StatusCodeEnum.InternalServerError,
                        Data=false
                    };
                }
                var catamaranViewModel = await _catamaranService.GetCatamaranByModel(rentalViewModel.CatamaranViewModel.Model);
                if (catamaranViewModel.Data==null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description= $"ошибка обратитесь к разработчикам",
                        StatusCode = StatusCodeEnum.InternalServerError,
                        Data=false
                    };
                }
                if (catamaranViewModel.Data.IsAvailable==false)
                {
                    var catamaranPurchaseAttempt = _catamaranRepository.GetAll().FirstOrDefault(c => c.Model==rentalViewModel.CatamaranViewModel.Model);
                    var purchaseAttempt = new PurchaseAttempt()
                    {
                        IsSuccessful=false,
                        PurchaseDate= rentalViewModel.StartTime,
                        ErrorMessage = "Данный катамаран занят билет не создан, выберете другой",
                        CatamaranId = catamaranPurchaseAttempt.Id
                    };
                    await _purchaseAttempRepository.Create(purchaseAttempt);
                    return new BaseResponse<bool>()
                    {
                        Description="Данный катамаран занят билет не создан, выберете другой",
                        StatusCode=Domain.Enum.StatusCodeEnum.NotFound,
                        Data=false,
                    };
                }
                var rental = _mapper.Map<Rental>(rentalViewModel);
                var catamaran = _mapper.Map<Catamaran>(catamaranViewModel.Data);
                var timeDifference = rental.EndTime-rental.StartTime;
                //rental.Catamaran=catamaran;
                rental.CatamaranId=catamaran.Id;
                rental.TotalCost = catamaran.PricePerHour * (int)timeDifference.TotalHours;
                rental.UserId=user.Data.Id;
                await _rentalRepository.Create(rental);
                await _ticketService.CreateTicket(rentalViewModel);
                await _purchaseService.CreatePurchase(rentalViewModel);
                return new BaseResponse<bool>()
                {
                    Description="Билет создан",
                    StatusCode=Domain.Enum.StatusCodeEnum.OK,
                    Data=true,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description= $"[CreateRental]: {ex.Message}",
                    StatusCode = StatusCodeEnum.InternalServerError
                };
            }   
        }
    }
}
