using AutoMapper;
using Contact.API.Dtos;
using Contact.API.Models;

namespace Contact.API.AutoMappers
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<ContactDto, ContactModel>().ReverseMap();
        }
    }
}
