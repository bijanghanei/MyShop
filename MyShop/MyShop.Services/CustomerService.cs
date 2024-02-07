using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Services
{
    public class CustomerService : ICustomerService
    {
        private IRepository<Customer> customerRepository;
        public CustomerService(IRepository<Customer> customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public void CreateCustomer(Customer customer)
        {
                customerRepository.Insert(customer);
                customerRepository.Commit();
        }
    }
}
