using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.EF.Application.Customers.Queries.FindCustomersByName;
using Northwind.EF.Application.Customers.Queries.GetCustomersList;

namespace Northwind.EF.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<CustomersListViewModel> List(CancellationToken ct)
        {
            return await _mediator.Send(new GetCustomersListQuery(), ct);
        }


        [Route("findbyname/{name}")]
        public async Task<CustomersListViewModel> FindByName([FromRoute] string name, CancellationToken ct)
        {
            return await _mediator.Send(new FindCustomersByName(name), ct);
        }
    }
}