using AutoMapper;
using Paint.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Paint.DTO
{
    public class PaintProfile : Profile
    {
        public PaintProfile()
        {
            CreateMap<Client, ClientItem>()
                .ForMember(d => d.ClientType, opt => opt.MapFrom(src => src.ClientType.Name));

            CreateMap<Client, ClientDetails>()
                .ForMember(d => d.ClientType, opt => opt.MapFrom(src => src.ClientType.Name))
                .ForMember(d => d.BillingAddress, opt => opt.MapFrom(src => src.BillingAddress.GetFormattedSiteAddress()));

            CreateMap<Job, JobItem>()
                .ForMember(d => d.Address, opt => opt.MapFrom(src => src.Address.Line1))
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(d => d.State, opt => opt.MapFrom(src => src.Address.State))
                .ForMember(d => d.Zip, opt => opt.MapFrom(src => src.Address.ZipCode));
        }
    }


}
