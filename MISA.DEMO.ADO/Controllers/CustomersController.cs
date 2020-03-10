using MISA.DEMO.ADO.Data;
using MISA.DEMO.ADO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MISA.DEMO.ADO.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        private DataAccess _dataAccess;
        public CustomersController()
        {
            _dataAccess = new DataAccess();
        }
        [Route("")]
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
            IEnumerable<Customer> customers = _dataAccess.GetCustomers();
            return customers;
        }
        [Route("{customerID}")]
        public IEnumerable<Customer> GetCustomerById(Guid customerID)
        {
            var customer = _dataAccess.GetCustomerById(customerID);
            return customer;
        }
        [HttpPost]
        [Route("")]
        public async Task<int> InsertCustomer([FromBody]Customer customer)
        {
            //await Task.Delay(2000);
            var result = _dataAccess.InsertCustomer(customer);
            return await Task.FromResult(result);
        }
        [HttpDelete]
        [Route("{customerID}")]
        public int DeleteCustomer(Guid customerID)
        {
            var result = _dataAccess.DeleteCustomer(customerID);
            return result;
        }
    }
}
