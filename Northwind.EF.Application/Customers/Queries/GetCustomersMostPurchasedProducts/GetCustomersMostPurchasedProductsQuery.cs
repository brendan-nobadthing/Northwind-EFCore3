using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.EF.Application.Interfaces;
using Northwind.EF.Domain.Models;

namespace Northwind.EF.Application.Customers.Queries.GetCustomersMostPurchasedProducts
{
    public class GetCustomersMostPurchasedProductsQuery : IRequest<IEnumerable<CustomersMostPurchasedViewModel>>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }

    public class GetCustomersMostPurchasedProductQueryHandler : IRequestHandler<GetCustomersMostPurchasedProductsQuery, IEnumerable<CustomersMostPurchasedViewModel>>
    {
        private readonly INorthwindDbContext _context;

        public GetCustomersMostPurchasedProductQueryHandler(INorthwindDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomersMostPurchasedViewModel>> Handle(GetCustomersMostPurchasedProductsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<CustomersMostPurchasedProducts>()
                .OrderByDescending(c => c.QuantityPurchased)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .Select(r => new CustomersMostPurchasedViewModel
                {
                    CompanyName = r.CompanyName,
                    CustomerId = r.CustomerID,
                    ProductId = r.ProductID,
                    ProductName = r.ProductName,
                    QuantityPurchased = r.QuantityPurchased
                })
                .ToListAsync(cancellationToken);
        }
    }
}
