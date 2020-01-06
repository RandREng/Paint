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
                .ForMember(d => d.ProjectManager, opt => opt.MapFrom(src => src.Client != null ? src.Client.Name : null)); ;
            CreateMap<Job, JobItem2>();


            CreateMap<BidSheet, BidListItem>()
                .ForMember(d => d.Address, opt => opt.MapFrom(src => src.Job.Address))
                .ForMember(d => d.ProjectManager, opt => opt.MapFrom(src => src.Job != null ? src.Job.Client.Name : null));
            CreateMap<BidSheet, BidListItem2>()
                .ForMember(d => d.Address, opt => opt.MapFrom(src => src.Job.Address));

            CreateMap<BidSheet, Bid>()
                .ForMember(d => d.Address, opt => opt.MapFrom(src => src.Job.Address))
                .ForMember(d => d.ProgjectManager, opt => opt.MapFrom(src => src.Job.Client.Name));
            CreateMap<BidArea, BidAreaDto>();
            CreateMap<BidItem, BidItemDto>();

            CreateMap<Address, AddressDto>();
        }
    }


}
