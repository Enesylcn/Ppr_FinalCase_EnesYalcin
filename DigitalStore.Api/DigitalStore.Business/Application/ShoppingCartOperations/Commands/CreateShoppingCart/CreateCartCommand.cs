using AutoMapper;
using DigitalStore.Base;
using DigitalStore.Base.Response;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.ShoppingCartOperations.Commands.CreateShoppingCart
{
    public record CreateCartCommand(ShoppingCartRequest Request) : IRequest<ApiResponse<ShoppingCartResponse>>;

    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, ApiResponse<ShoppingCartResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ISessionContext sessionContext;
        private readonly UserManager<User> userManager;


        public CreateCartCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionContext sessionContext, UserManager<User> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.sessionContext = sessionContext;
            this.userManager = userManager;
        }

        public async Task<ApiResponse<ShoppingCartResponse>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<ShoppingCartRequest, ShoppingCart>(request.Request);
            mapped.UserId = sessionContext.Session.UserId;
            mapped.Name = $"{mapped.UserId}_{mapped.InsertDate}";

            mapped.ShoppingCartItems = new List<ShoppingCartItem>();
            float totalAmount = 0;
            float totalPointsEarned = 0;

            foreach (var productId in request.Request.ProductIds)
            {
                var product = await unitOfWork.ProductRepository.GetById(productId);
                if (product != null && product.Stock >= request.Request.Quantity)
                {
                    var itemTotal = product.Price * request.Request.Quantity;
                    totalAmount += itemTotal;

                    // Puan kazanımı
                    var pointsEarned = Math.Min(itemTotal * (product.PointsEarningPercentage), product.MaxPointsAmount);
                    totalPointsEarned += pointsEarned;

                    var cartItem = new ShoppingCartItem
                    {
                        ProductId = product.Id,
                        Product = product,
                        Quantity = request.Request.Quantity,
                        Price = product.Price,
                        Name = $"{product.Name}-{mapped.UserId}-{mapped.InsertDate}",
                        InsertUser = sessionContext.Session.UserName
                    };
                    mapped.ShoppingCartItems.Add(cartItem);
                }
                else
                {
                    // Yeterli stok yoksa, hata döndürülebilir
                    return new ApiResponse<ShoppingCartResponse>("Insufficient stock for product: " + product?.Name);
                }
            }

            mapped.TotalAmount = totalAmount;


            // Kupon uygulanması
            if (!string.IsNullOrEmpty(request.Request.CouponCode))
            {
                var coupon = await unitOfWork.CouponRepository.FirstOrDefaultAsync(c => c.Code == request.Request.CouponCode && c.IsActive);
                if (coupon != null && DateTime.Now >= coupon.ValidFrom && DateTime.Now <= coupon.ValidUntil)
                {
                    totalAmount -= coupon.DiscountPercentage / 100 * totalAmount;
                    mapped.CouponCode = request.Request.CouponCode;
                }
            }

            // Sepet toplamının setlenmesi
            mapped.CartAmount = totalAmount;

            await unitOfWork.ShoppingCartRepository.Insert(mapped);
            await unitOfWork.Complete();

            // Puan cüzdanına puan ekleme
            var user = await userManager.FindByIdAsync(sessionContext.Session.UserId);
            if (user != null)
            {
                user.PointsWallet += totalPointsEarned;
                await userManager.UpdateAsync(user);
            }

            var response = mapper.Map<ShoppingCartResponse>(mapped);
            return new ApiResponse<ShoppingCartResponse>(response);
        }
    }
}
