
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

            CreateMap<Product, ProductResponse>()
                     .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category)));
            CreateMap<ProductRequest, Product>();

            CreateMap<Order, OrderResponse>();
            CreateMap<OrderRequest, Order>();

            CreateMap<Order, OrderResponse>();
            CreateMap<OrderRequest, OrderResponse>();

            CreateMap<User, UserResponse>();
            CreateMap<UserRequest, User>();

            CreateMap<OrderDetail, OrderDetailResponse>();
            CreateMap<OrderDetailRequest, OrderDetail>();

            CreateMap<ProductCategory, ProductCategoryResponse>();
            CreateMap<ProductCategoryRequest, ProductCategory>();

            CreateMap<ShoppingCart, ShoppingCartResponse>();
            CreateMap<ShoppingCartRequest, ShoppingCart>();

            CreateMap<ShoppingCartItem, ShoppingCartItemResponse>();
            CreateMap<ShoppingCartItemRequest, ShoppingCartItem>();

            CreateMap<Coupon, CouponResponse>();
            CreateMap<CouponRequest, Coupon>();

        }

    }
}
