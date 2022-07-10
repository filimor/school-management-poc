using AutoMapper;
using SchoolManagement.Poc.Data;
using SchoolManagement.Poc.Models;

namespace SchoolManagement.Poc.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();
        }
    }
}
