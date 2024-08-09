using AutoMapper;
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

namespace DigitalStore.Business.Application.ShoppingCartItemOperations.Commands.CreateShoppingCartItem
{
    public record CreateCartItemCommand(ShoppingCartItemRequest Request) : IRequest<ApiResponse<ShoppingCartItemResponse>>;

    public class CreateCartItemCommandCommandHandler : IRequestHandler<CreateCartItemCommand, ApiResponse<ShoppingCartItemResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateCartItemCommandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<ShoppingCartItemResponse>> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<ShoppingCartItemRequest, ShoppingCartItem>(request.Request);
            await unitOfWork.ShoppingCartItemRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<ShoppingCartItemResponse>(mapped);
            return new ApiResponse<ShoppingCartItemResponse>(response);
        }
    }
}
