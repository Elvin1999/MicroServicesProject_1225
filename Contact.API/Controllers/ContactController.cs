using AutoMapper;
using Contact.API.Dtos;
using Contact.API.Infrastructure;
using Contact.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contact.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private IMapper _mapper;

        public ContactController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ContactDto Get(int id)
        {
            var item=_contactService.GetContactById(id);
            var dto=_mapper.Map<ContactDto>(item);
            return dto;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _contactService.Delete(id);
            return NoContent();
        }

        [HttpGet("")]
        public List<ContactDto> GetAll()
        {
            var items=_contactService.GetAll();
            var dtos=_mapper.Map<List<ContactDto>>(items);
            return dtos;    
        }


        [HttpPost]
        public ContactDto Add(ContactDto dto)
        {
            var model=_mapper.Map<ContactModel>(dto);
            _contactService.Add(model);
            var addedDto = _mapper.Map<ContactDto>(model);
            return addedDto;
        }
    }
}
