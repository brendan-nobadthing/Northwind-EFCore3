using AutoMapper;

namespace Northwind.EF.Application.Interfaces.Mapping
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile configuration);
    }
}
