using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Data.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.ShoppingCartItemOperations.Commands.DeleteShoppingCartItem
{
    public record DeleteCartItemCommand(long ShoppingCartItemId) : IRequest<ApiResponse>;
    public class DeleteCartItemCommandHandler : IRequestHandler<DeleteCartItemCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteCartItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.ShoppingCartItemRepository.Delete(request.ShoppingCartItemId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
