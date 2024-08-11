using AutoMapper;
using DigitalStore.Base;
using DigitalStore.Base.Response;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
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

        public CreateCartCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionContext sessionContext)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.sessionContext = sessionContext;
        }

        public async Task<ApiResponse<ShoppingCartResponse>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<ShoppingCartRequest, ShoppingCart>(request.Request);
            mapped.UserId = sessionContext.Session.UserId;
            mapped.ShoppingCartItems = new List<ShoppingCartItem>();

            foreach (var productId in request.Request.ProductIds)
            {
                var product = await unitOfWork.ProductRepository.GetById(productId);
                if (product != null)
                {
                    var cartItem = new ShoppingCartItem
                    {
                        ProductId = product.Id,
                        Product = product,
                        Quantity = 1, // Ürün adedi, isterseniz request'ten de alabilirsiniz
                        Price = product.Price
                    };
                    mapped.ShoppingCartItems.Add(cartItem);
                }
            }

            await unitOfWork.ShoppingCartRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<ShoppingCartResponse>(mapped);
            return new ApiResponse<ShoppingCartResponse>(response);
        }
        //public async Task<ApiResponse<ShoppingCartResponse>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        //{
        //    var mapped = mapper.Map<ShoppingCartRequest, ShoppingCart>(request.Request);
        //    mapped.UserId = sessionContext.Session.UserId;
        //    await unitOfWork.ShoppingCartRepository.Insert(mapped);
        //    await unitOfWork.Complete();

        //    var response = mapper.Map<ShoppingCartResponse>(mapped);
        //    return new ApiResponse<ShoppingCartResponse>(response);
        //}
    }
}
