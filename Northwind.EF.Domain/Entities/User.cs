using Northwind.EF.Domain.ValueObjects;

namespace Northwind.EF.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public AdAccount AdAccount { get; set; }
    }
}
