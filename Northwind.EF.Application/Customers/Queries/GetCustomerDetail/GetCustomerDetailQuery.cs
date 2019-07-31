namespace Northwind.EF.Application.Customers.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQuery : IRequest<CustomerDetailModel>
    {
        public string Id { get; set; }
    }
}
