using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using NorthwindTraders.Application.Customers.Queries.GetCustomersList;
using NorthwindTraders.Application.Customers.Queries.GetCustomerDetail;
using NorthwindTraders.Application.Customers.Commands.CreateCustomer;
using NorthwindTraders.Application.Customers.Commands.UpdateCustomer;
using NorthwindTraders.Application.Customers.Commands.DeleteCustomer;

namespace NorthwindTraders.Controllers
{
    [Produces("application/json")]
    [Route("api/Customers")]
    public class CustomersController : Controller
    {
        public readonly IGetCustomersListQuery _getCustomersListQuery;
        public readonly IGetCustomerDetailQuery _getCustomerDetailQuery;
        public readonly ICreateCustomerCommand _createCustomerCommand;
        public readonly IUpdateCustomerCommand _updateCustomerCommand;
        public readonly IDeleteCustomerCommand _deleteCustomerCommand;

        public CustomersController(
            IGetCustomersListQuery getCustomersListQuery,
            IGetCustomerDetailQuery getCustomerDetailQuery,
            ICreateCustomerCommand createCustomerCommand,
            IUpdateCustomerCommand updateCustomerCommand,
            IDeleteCustomerCommand deleteCustomerCommand)
        {
            _getCustomersListQuery = getCustomersListQuery;
            _getCustomerDetailQuery = getCustomerDetailQuery;
            _createCustomerCommand = createCustomerCommand;
            _updateCustomerCommand = updateCustomerCommand;
            _deleteCustomerCommand = deleteCustomerCommand;
        }

        // GET api/customers
        [HttpGet]
        public async Task<IEnumerable<CustomerListModel>> Get()
        {
            return await _getCustomersListQuery.Execute();
        }
        
        // GET api/customers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var customer = await _getCustomerDetailQuery.Execute(id);
            if (customer == null)
            {
                return NotFound();
            }

            return new ObjectResult(customer);
        }

        // POST api/customers
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateCustomerModel customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _createCustomerCommand.Execute(customer);

            return CreatedAtRoute("Create", new { customer.Id }, customer);
        }

        // PUT api/customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody]UpdateCustomerModel customer)
        {
            if (customer == null || customer.Id != id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _updateCustomerCommand.Execute(customer);

            return new NoContentResult();
        }

        // DELETE api/customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _deleteCustomerCommand.Execute(id);

            return new NoContentResult();
        }
    }
}