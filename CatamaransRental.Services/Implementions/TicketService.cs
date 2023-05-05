using AutoMapper;
using CatamaransRental.DAL.Interfaces;
using CatamaransRental.DAL.Repositories;
using CatamaransRental.Domain;
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
    public class TicketService:ITicketService
    {
        private readonly IBaseRepository<Ticket> _ticketRepository;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Rental> _rentalRepository;
        private readonly ICatamaranService _catamaranService;

        public TicketService(IBaseRepository<Ticket> ticketRepository,IMapper mapper,
            IBaseRepository<Rental> rentalRepository,ICatamaranService catamaranService)
        {
            _ticketRepository=ticketRepository;
            _mapper=mapper;
            _rentalRepository=rentalRepository;
            _catamaranService=catamaranService;
        }

        public async Task<IBaseResponse<bool>> CreateTicket(RentalViewModel rentalViewModel)
        {
           
            try
            {
                var respone = await _rentalRepository.GetAll().FirstOrDefaultAsync(r => r.StartTime==rentalViewModel.StartTime);
                var timeDifference = respone.EndTime-respone.StartTime;
                var ticket = new Ticket()
                {
                    CatamaranId = respone.CatamaranId,
                    RentalId=respone.Id,
                    PurchaseDate = respone.StartTime,
                    DurationInHours = (int)timeDifference.TotalHours,
                    TotalCost = respone.TotalCost,
                    IsUsed = false,
                };
                await _ticketRepository.Create(ticket);
                return new BaseResponse<bool>()
                {
                    Description="Билет создан, на данный катамаран",
                    StatusCode=Domain.Enum.StatusCodeEnum.NotFound,
                    Data=true,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {

                    StatusCode=Domain.Enum.StatusCodeEnum.InternalServerError,
                    Description=$"Ошибка подключения + {ex.Message}",
                    Data=false
                };
            }
        }

        public IBaseResponse<IEnumerable<TicketViewModel>> GetAllTickets()
        {
            try
            {
                var tickets = _ticketRepository.GetAll().ToList();
                var catamaransViewModel = _catamaranService.GetAllCatamarans();
                var catamarans = _mapper.Map<IEnumerable<Catamaran>>(catamaransViewModel.Data);
                foreach (var ticket in tickets)
                {
                    ticket.Catamaran = catamarans.FirstOrDefault(c => c.Id==ticket.CatamaranId);
                }
                if (tickets == null)
                {
                    return new BaseResponse<IEnumerable<TicketViewModel>>()
                    {
                        StatusCode=Domain.Enum.StatusCodeEnum.NotFound,
                        Description="Найдено ноль элементов"
                    };
                }
                var ticketsViewModel = _mapper.Map<IEnumerable<TicketViewModel>>(tickets);
                return new BaseResponse<IEnumerable<TicketViewModel>>()
                {
                    Data = ticketsViewModel,
                    StatusCode=Domain.Enum.StatusCodeEnum.OK,
                    Description="Найден один элемент"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<TicketViewModel>>()
                {
                    Description= $"[GetCatamaranById]: {ex.Message}",
                    StatusCode = StatusCodeEnum.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<TicketViewModel>> GetTicketByDate(DateTime purchaseDate)
        {
            try
            {
                var ticket = await _ticketRepository.GetAll().FirstOrDefaultAsync(x => x.PurchaseDate == purchaseDate);
                var catamaransViewModel = _catamaranService.GetAllCatamarans();
                var catamarans = _mapper.Map<IEnumerable<Catamaran>>(catamaransViewModel.Data);
                if (ticket == null)
                {
                    return new BaseResponse<TicketViewModel>()
                    {
                        StatusCode=Domain.Enum.StatusCodeEnum.NotFound,
                        Description="Найдено ноль элементов"
                    };
                }
                ticket.Catamaran = catamarans.FirstOrDefault(c => c.Id==ticket.CatamaranId);
                
                var ticketViewModel = _mapper.Map<TicketViewModel>(ticket);
                return new BaseResponse<TicketViewModel>()
                {
                    Data = ticketViewModel,
                    StatusCode=Domain.Enum.StatusCodeEnum.OK,
                    Description="Найден один элемент"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TicketViewModel>()
                {
                    Description= $"[GetCatamaranById]: {ex.Message}",
                    StatusCode = StatusCodeEnum.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> RegisterTicket(DateTime purchaseDate)
        {
            try
            {
                var ticket = await _ticketRepository.GetAll().AsNoTracking().FirstOrDefaultAsync(x => x.PurchaseDate == purchaseDate);
                //var catamaransViewModel = _catamaranService.GetAllCatamarans();
                //var catamarans = _mapper.Map<IEnumerable<Catamaran>>(catamaransViewModel.Data);
                if (ticket == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        StatusCode=Domain.Enum.StatusCodeEnum.NotFound,
                        Description="Найдено ноль элементов"
                    };
                }
                //ticket.Catamaran = catamarans.FirstOrDefault(c => c.Id==ticket.CatamaranId);
                //var catamaran = _catamaranService.GetAllCatamarans().Data.FirstOrDefault(c => c.Id==ticket.CatamaranId);
                //var updateCatamaran = _mapper.Map<Catamaran>(catamaran);
                //updateCatamaran.Rentals=null;
                //updateCatamaran.IsAvailable=false;
                ticket.IsUsed=true;
                await _ticketRepository.Update(ticket);
                await _catamaranService.Update(ticket.CatamaranId);
                var ticketViewModel = _mapper.Map<TicketViewModel>(ticket);
                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode=Domain.Enum.StatusCodeEnum.OK,
                    Description="Регистрация прошла успешно"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description= $"[RegisterTicket]: {ex.Message}",
                    StatusCode = StatusCodeEnum.InternalServerError
                };
            }
        }
    }
}
