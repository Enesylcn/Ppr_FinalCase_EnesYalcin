using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Data.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.ShoppingCartOperations.Commands.DeleteShoppingCart
{
    public record DeleteCartCommand(long ShoppingCartId) : IRequest<ApiResponse>;
    public class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteCartCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.ShoppingCartRepository.Delete(request.ShoppingCartId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
