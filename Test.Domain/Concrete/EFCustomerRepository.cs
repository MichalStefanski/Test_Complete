using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Abstract;
using Test.Domain.Entities;

namespace Test.Domain.Concrete
{
    public class EFCustomerRepository : ICustomerRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Customer> Customers
        {
            get { return context.Customers; }
        }

        public void SaveCustomer(Customer customer)
        {
            if (customer.CustomerID == 0)
            {
                context.Customers.Add(customer);
            }
            else
            {
                Customer dbEntry = context.Customers.Find(customer.CustomerID);
                if (dbEntry != null)
                {
                    dbEntry.CustomerID = customer.CustomerID;
                    dbEntry.PhoneNumber = customer.PhoneNumber;
                }                
            }
            context.SaveChanges();
        }
    }
}
