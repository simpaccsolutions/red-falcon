using AutoMapper;
using RedFalcon.Application.DTOs;
using RedFalcon.Domain.Entities;

namespace RedFalcon.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contact, CreateContactDTO>().ReverseMap();
            CreateMap<Contact, UpdateContactDTO>().ReverseMap();
            CreateMap<Contact, ViewContactDTO>();
        }
    }
}
