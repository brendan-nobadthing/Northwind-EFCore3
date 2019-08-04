using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.EF.Application.Customers.Queries.GetCustomersMostPurchasedProducts
{
    public class CustomersMostPurchasedViewModel
    {
        public string CustomerId { get; set; }

        public string CompanyName { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int QuantityPurchased { get; set; }
    }
}
