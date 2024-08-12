using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Base;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Azure;
using DigitalStore.Data.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Iyzipay.Model.V2.Subscription;

namespace DigitalStore.Business.Application.ShoppingCartOperations.Queries.AddToCart
{
    public record AddToCartQuery(string userId, ShoppingCartRequest Request) : IRequest<ApiResponse<ShoppingCart>>;

    public class AddToCartQueryHandler : IRequestHandler<AddToCartQuery, ApiResponse<ShoppingCart>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ISessionContext sessionContext;

        public AddToCartQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionContext sessionContext)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.sessionContext = sessionContext;
        }


        public async Task<ApiResponse<ShoppingCart>> Handle(AddToCartQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await unitOfWork.ShoppingCartRepository
                .Query(sc => sc.UserId == request.userId)
                .Include(sc => sc.ShoppingCartItems)
                .ThenInclude(sci => sci.Product)
                .FirstOrDefaultAsync();
            if (shoppingCart != null)
            {
                float totalAmount = 0;
                float totalPointsEarned = 0;


                //Eğer ürün daha önceden sepette varsa, sıra numarası bulunur ve index içine konur.
                //Eğer ürün daha önceden sepette yoksa, sıra numarası -1 olarak döner.

                foreach (var item in request.Request.ProductIds)
                {
                    var product = await unitOfWork.ProductRepository.GetById(item);
                    if (product != null && product.Stock >= request.Request.Quantity)
                    {
                        var itemTotal = product.Price * request.Request.Quantity;
                        totalAmount += itemTotal;

                        // Puan kazanımı
                        var pointsEarned = Math.Min(itemTotal * (product.PointsEarningPercentage), product.MaxPointsAmount);
                        totalPointsEarned += pointsEarned;


                        var index = shoppingCart.ShoppingCartItems.FindIndex(x => x.ProductId == item);
                        if (index < 0)
                        {
                            shoppingCart.ShoppingCartItems.Add(new ShoppingCartItem
                            {
                                ProductId = product.Id,
                                Product = product,
                                Quantity = request.Request.Quantity,
                                Price = product.Price,
                                Name = $"{product.Name}-{sessionContext.Session.UserId}-{DateTime.Now}",
                                InsertUser = sessionContext.Session.UserName
                            });
                        }
                        else
                        {
                            shoppingCart.ShoppingCartItems[index].Quantity += request.Request.Quantity;
                        }
                    }
                    else
                    {
                        // Yeterli stok yoksa, hata döndürülebilir
                        return new ApiResponse<ShoppingCart>("Insufficient stock for product: " + product?.Name);
                    }
                }

                shoppingCart.TotalAmount = totalAmount;

                // Kupon uygulanması
                if (!string.IsNullOrEmpty(request.Request.CouponCode))
                {
                    var coupon = await unitOfWork.CouponRepository.FirstOrDefaultAsync(c => c.Code == request.Request.CouponCode && c.IsActive);
                    if (coupon != null && DateTime.Now >= coupon.ValidFrom && DateTime.Now <= coupon.ValidUntil)
                    {
                        totalAmount -= coupon.DiscountPercentage / 100 * totalAmount;
                        shoppingCart.CouponCode = request.Request.CouponCode;
                    }
                }

                shoppingCart.CartAmount = totalAmount;

                unitOfWork.ShoppingCartRepository.Update(shoppingCart);
                await unitOfWork.Complete();
                return new ApiResponse<ShoppingCart>(shoppingCart);

            }
            return new ApiResponse<ShoppingCart>("Bir hata oluştu");
        }
    }
}
