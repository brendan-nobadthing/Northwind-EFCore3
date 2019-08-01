using System;
using System.Collections.Generic;

namespace Northwind.EF.Application.Customers.Queries.GetCustomerOrders
{
    public class CustomerOrdersModel
    {

        public string CustomerId { get; set; }
        public string Name { get; set; }

        public int CountOrders { get; set; }

        public IEnumerable<CustomerOrderModel> Orders { get; set; }

    }


    public class CustomerOrderModel
    {
        public int OrderId { get; set; }

        public int? EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public DateTime? OrderDate { get; set; }
    }

}
