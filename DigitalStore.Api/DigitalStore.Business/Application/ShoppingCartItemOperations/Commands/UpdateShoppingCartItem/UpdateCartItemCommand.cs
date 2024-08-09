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

namespace DigitalStore.Business.Application.ShoppingCartItemOperations.Commands.UpdateShoppingCartItem
{
    public record UpdateCartItemCommand(long ShoppingCartItemId, ShoppingCartItemRequest Request) : IRequest<ApiResponse>;
    public class UpdateCartItemCommandHandler : IRequestHandler<UpdateCartItemCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateCartItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<ShoppingCartItemRequest, ShoppingCartItem>(request.Request);
            mapped.Id = request.ShoppingCartItemId;
            unitOfWork.ShoppingCartItemRepository.Update(mapped);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
