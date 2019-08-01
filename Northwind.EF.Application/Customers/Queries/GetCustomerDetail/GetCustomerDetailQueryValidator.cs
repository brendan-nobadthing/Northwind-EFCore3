using FluentValidation;

namespace Northwind.EF.Application.Customers.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQueryValidator : AbstractValidator<GetCustomerDetailQuery>
    {
        public GetCustomerDetailQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty().Length(5);
        }
    }
}
