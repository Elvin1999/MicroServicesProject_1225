using Contact.API.Infrastructure;
using Contact.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.API.Services
{
    public class ContactService : IContactService
    {
        public static List<ContactModel> AllContacts { get; set; }
        = new List<ContactModel>
        {

            new ContactModel
            {
                Id=new Random().Next(1,100000),
                Firstname="Aysel",
                 Lastname="Eliyeva"
            },
            new ContactModel
            {
                Id=new Random().Next(1,100000),
                Firstname="Tural",
                 Lastname="Mammadov"
            },
                new ContactModel
            {
                Id=new Random().Next(1,100000),
                Firstname="Ali",
                 Lastname="Aliyev"
            }
        };
        public void Add(ContactModel model)
        {
            model.Id = new Random().Next(1, 100000);
            AllContacts.Add(model);
        }

        public void Delete(int id)
        {
            var item=AllContacts.FirstOrDefault(x=>x.Id == id);
            if (item != null)
            {
                AllContacts.Remove(item);
            }
        }

        public List<ContactModel> GetAll()
        {
            return AllContacts;
        }

        public ContactModel GetContactById(int id)
        {
            return AllContacts.FirstOrDefault(x => x.Id == id);
        }
    }
}
