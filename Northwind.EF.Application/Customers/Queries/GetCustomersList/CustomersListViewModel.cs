using System.Collections.Generic;

namespace Northwind.EF.Application.Customers.Queries.GetCustomersList
{
    public class CustomersListViewModel
    {
        public IList<CustomerLookupModel> Customers { get; set; }
    }
}
