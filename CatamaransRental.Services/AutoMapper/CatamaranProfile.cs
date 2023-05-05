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
    public class CatamaranProfile:Profile
    {
        public CatamaranProfile() 
        {
            CreateMap<Catamaran, CatamaranViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity))
                .ForMember(dest => dest.PricePerHour, opt => opt.MapFrom(src => src.PricePerHour))
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable))
                .ForMember(dest => dest.Rentals, opt => opt.MapFrom(src => src.Rentals))
                .ForMember(dest=>dest.Image,opt=>opt.MapFrom(src=>src.Image))
                .ReverseMap();
        }
    }
}
