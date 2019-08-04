using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.EF.Domain.Models
{
    public class CustomersMostPurchasedProducts
    {
        public string CustomerID { get; set; }

        public string CompanyName { get; set; }

        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public int QuantityPurchased { get; set; }
    }
}
