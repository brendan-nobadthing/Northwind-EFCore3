namespace Northwind.EF.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest
    {
        public string Id { get; set; }
    }
}
