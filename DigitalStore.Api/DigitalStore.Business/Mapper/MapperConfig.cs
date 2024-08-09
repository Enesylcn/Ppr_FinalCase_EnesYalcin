
using AutoMapper;
using DigitalStore.Data.Domain;
using DigitalStore.Schema;
using System.Diagnostics.Metrics;

namespace DigitalStore.Business.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Category, CategoryResponse>();
            CreateMap<CategoryRequest, Category>();

            CreateMap<Product, ProductResponse>();
            CreateMap<ProductRequest, Product>();

            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
            CreateMap<OrderRequest, Order>();

            //CreateMap<CustomerAddress, CustomerAddressResponse>()
            //    .ForMember(dest => dest.CustomerIdentityNumber, opt => opt.MapFrom(src => src.Customer.IdentityNumber))
            //    .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.Customer.CustomerNumber))
            //    .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName));
            //CreateMap<CustomerAddressRequest, CustomerAddress>();


        }

    }
}
