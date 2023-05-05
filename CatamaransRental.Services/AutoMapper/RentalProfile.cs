using AutoMapper;
using CatamaransRental.Domain.Models;
using CatamaransRental.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.Services.AutoMapper
{
    public class RentalProfile:Profile
    {
        public RentalProfile() 
        {
            CreateMap<Rental, RentalViewModel>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.TotalCost))
                .ForMember(dest => dest.CatamaranId, opt => opt.MapFrom(src => src.CatamaranId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ReverseMap();
        }
    }
}
