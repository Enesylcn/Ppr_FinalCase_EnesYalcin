
using AutoMapper;
using DigitalStore.Data.Domain;
using DigitalStore.Schema;
using Microsoft.AspNetCore.Builder;
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

            CreateMap<Order, OrderResponse>();
            CreateMap<OrderRequest, Order>();

            CreateMap<Order, OrderResponse>();
            CreateMap<OrderRequest, OrderResponse>();

            CreateMap<User, AuthRequest>();
            CreateMap<AuthResponse, User>();

            CreateMap<OrderDetail, OrderDetailResponse>();
            CreateMap<OrderDetailRequest, OrderDetail>();

            CreateMap<ProductCategory, ProductCategoryResponse>();
            CreateMap<ProductCategoryRequest, ProductCategory>();

            CreateMap<ShoppingCart, ShoppingCartResponse>();
            CreateMap<ShoppingCartRequest, ShoppingCart>();

            CreateMap<ShoppingCartItem, ShoppingCartItemResponse>();
            CreateMap<ShoppingCartItemRequest, ShoppingCartItem>();


            //CreateMap<CustomerAddress, CustomerAddressResponse>()
            //    .ForMember(dest => dest.CustomerIdentityNumber, opt => opt.MapFrom(src => src.Customer.IdentityNumber))
            //    .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.Customer.CustomerNumber))
            //    .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName));
            //CreateMap<CustomerAddressRequest, CustomerAddress>();


        }

    }
}
