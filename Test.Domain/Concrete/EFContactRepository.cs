using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Abstract;
using Test.Domain.Entities;

namespace Test.Domain.Concrete
{
     public class EFContactRepository : IContactRepository
     {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Contact> Contacts
        {
            get { return context.Contacts; }
        }

        public void SaveContact(Contact contact, Customer customer)
        {
            Contact dbEntry = context.Contacts.Find(customer.CustomerID);
            if (dbEntry == null)
            {
                context.Contacts.Add(contact);
            }
            else
            {                
                if (dbEntry != null)
                {
                    dbEntry.FirstName = contact.FirstName;
                    dbEntry.LastName = contact.LastName;
                    dbEntry.PostCode = contact.PostCode;
                    dbEntry.Email = contact.Email;
                }                
            }
            context.SaveChanges();
        }
     }
}
