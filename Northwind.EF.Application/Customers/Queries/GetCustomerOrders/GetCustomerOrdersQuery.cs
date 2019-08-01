//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading.Tasks;
//using AutoMapper;
//using AutoMapper.QueryableExtensions;
//using Microsoft.EntityFrameworkCore;

//namespace Northwind.EF.Application.Customers.Queries.GetCustomerOrders
//{
//    public class GetCustomerOrdersQuery : IGetCustomerOrdersQuery
//    {
//        private readonly NorthwindContext _context;

//        public GetCustomerOrdersQuery(NorthwindContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<CustomerOrdersModel>> Execute(CustomerOrdersQueryModel queryModel)
//        {
//            var efQuery = _context.Customers.Where(c => true);

//            efQuery = ApplyFilters(efQuery, queryModel);
//            efQuery = ApplyPaging(efQuery, queryModel);

//            return await SimpleSelect(efQuery);
       
//            // return await ToListSelect(efQuery);
//            // return await SelectAnon(efQuery);
//            // return await ReusableProjection(efQuery);
//            // return await ExtensionMethod(efQuery);
//            // return await AutoMapper(efQuery);


//            // Note: Show Linqkit predicate builder
//            // http://www.albahari.com/nutshell/predicatebuilder.aspx 

//        }


//        private async Task<IEnumerable<CustomerOrdersModel>> SimpleSelect(IQueryable<Customer> efQuery)
//        {
//            return await efQuery
//                .Select(c => new CustomerOrdersModel()
//                {
//                    Name = c.CompanyName,
//                    CustomerId = c.CustomerId,
//                    CountOrders = c.Orders.Count(),
//                    Orders = c.Orders.Select(o => new CustomerOrderModel()
//                    {
//                        OrderId = o.OrderId,
//                        EmployeeId = o.EmployeeId,
//                        EmployeeName = o.Employee != null
//                            ? o.Employee.FirstName + " " + o.Employee.LastName
//                            : string.Empty,
//                        OrderDate = o.OrderDate
//                    })
//                }).ToListAsync();
//        }




//        private async Task<IEnumerable<CustomerOrdersModel>> ToListSelect(IQueryable<Customer> efQuery)
//        {
//            var list = await efQuery
//                .Include(c => c.Orders).ThenInclude(o => o.Employee)
//                .ToListAsync();
//            return list.Select(c => new CustomerOrdersModel()
//            {
//                Name = c.CompanyName,
//                CustomerId = c.CustomerId,
//                CountOrders = c.Orders.Count(),
//                Orders = c.Orders.Select(o => new CustomerOrderModel()
//                {
//                    OrderId = o.OrderId,
//                    EmployeeId = o.EmployeeId,
//                    EmployeeName = o.Employee != null
//                        ? o.Employee.FirstName + " " + o.Employee.LastName
//                        : string.Empty,
//                    OrderDate = o.OrderDate.GetValueOrDefault().ToLocalTime() // This operation not supported by EF
//                })
//            });
//        }



//        private async Task<IEnumerable<CustomerOrdersModel>> SelectAnon(IQueryable<Customer> efQuery)
//        {
//            var list = await efQuery.Select(c => new 
//            {
//                Name = c.CompanyName,
//                CustomerId = c.CustomerId,
//                CountOrders = c.Orders.Count(),
//                Orders = c.Orders.Select(o => new 
//                {
//                    OrderId = o.OrderId,
//                    EmployeeId = o.EmployeeId,
//                    EmployeeName = o.Employee != null
//                        ? o.Employee.FirstName + " " + o.Employee.LastName
//                        : string.Empty,
//                    OrderDate = o.OrderDate
//                })
//            }).ToListAsync();
//            return list.Select(c => new CustomerOrdersModel()
//            {
//                Name = c.Name,
//                CustomerId = c.CustomerId,
//                CountOrders = c.Orders.Count(),
//                Orders = c.Orders.Select(o => new CustomerOrderModel()
//                {
//                    OrderId = o.OrderId,
//                    EmployeeId = o.EmployeeId,
//                    EmployeeName = o.EmployeeName,
//                    OrderDate = o.OrderDate.GetValueOrDefault().ToLocalTime() // This operation not supported by EF
//                })
//            });
//        }



//        private async Task<IEnumerable<CustomerOrdersModel>> ReusableProjection(IQueryable<Customer> efQuery)
//        {
//            return await efQuery.Select(CustomerToOrdersModelProjection)
//                .ToListAsync();
//        }


//        public static Expression<Func<Customer, CustomerOrdersModel>> CustomerToOrdersModelProjection =>
//            c => new CustomerOrdersModel()
//            {
//                Name = c.CompanyName,
//                CustomerId = c.CustomerId,
//                CountOrders = c.Orders.Count(),
//                Orders = c.Orders.Select(o => new CustomerOrderModel()
//                {
//                    OrderId = o.OrderId,
//                    EmployeeId = o.EmployeeId,
//                    EmployeeName = o.Employee != null
//                        ? o.Employee.FirstName + " " + o.Employee.LastName
//                        : string.Empty,
//                    OrderDate = o.OrderDate
//                })
//            };


//        private async Task<IEnumerable<CustomerOrdersModel>> ExtensionMethod(IQueryable<Customer> efQuery)
//        {
//            return await efQuery
//                .ToViewModel()
//                .ToListAsync();
//        }



//        private async Task<IEnumerable<CustomerOrdersModel>> AutoMapper(IQueryable<Customer> efQuery)
//        {
//            return await efQuery
//                .ProjectTo<CustomerOrdersModel>(MapperConfig)
//                .ToListAsync();
//        }


//        private static MapperConfiguration _mapperConfig = null;

//        private static MapperConfiguration MapperConfig
//        {
//            get
//            {
//                if (_mapperConfig == null)
//                {
//                    _mapperConfig = new MapperConfiguration(cfg =>
//                    {
//                        cfg.CreateMap<Order, CustomerOrderModel>()
//                            .ForMember(o => o.EmployeeName,
//                                opt => opt.MapFrom(o => o.Employee.FirstName + " automapped " + o.Employee.LastName))
//                            .ForMember(o => o.OrderDate,
//                                opt => opt.MapFrom(o => o.OrderDate.GetValueOrDefault().ToLocalTime()));

//                        cfg.CreateMap<Customer, CustomerOrdersModel>()
//                            .ForMember(d => d.Name, opt => opt.MapFrom(c => c.CompanyName))
//                            .ForMember(d => d.CountOrders, opt => opt.MapFrom(c => c.Orders.Count()));
                        
//                    });
//                }
//                return _mapperConfig;
//            }
//        }


//        private IQueryable<Customer> ApplyPaging(IQueryable<Customer> efQuery, CustomerOrdersQueryModel queryModel)
//        {
//            if (queryModel.PageIndex.HasValue && queryModel.PageSize.HasValue)
//            {
//                return efQuery
//                    .Skip(queryModel.PageIndex.Value * queryModel.PageSize.Value)
//                    .Take(queryModel.PageSize.Value)
//                    .OrderBy(c => c.CompanyName);
//            }
//            return efQuery;
//        }

//        private IQueryable<Customer> ApplyFilters(IQueryable<Customer> efQuery, GetCustomerOrders request)
//        {
//            if (!string.IsNullOrWhiteSpace(request.NameSearch))
//            {
//                efQuery = efQuery.Where(c => c.CompanyName.Contains(request.NameSearch));
//            }
//            return efQuery;
//        }


//    }


//    public static class CustomerExtensions
//    {


//        public static IQueryable<CustomerOrdersModel> ToViewModel (this IQueryable<Customer> queryable)
//        {
//            return queryable.Select(c => new CustomerOrdersModel()
//            {
//                Name = c.CompanyName,
//                CustomerId = c.CustomerId,
//                CountOrders = c.Orders.Count(),
//                Orders = c.Orders.Select(o => new CustomerOrderModel()
//                {
//                    OrderId = o.OrderId,
//                    EmployeeId = o.EmployeeId,
//                    EmployeeName = o.Employee != null
//                        ? o.Employee.FirstName + " " + o.Employee.LastName
//                        : string.Empty,
//                    OrderDate = o.OrderDate
//                })
//            });
//        }
        
//    }

//}
