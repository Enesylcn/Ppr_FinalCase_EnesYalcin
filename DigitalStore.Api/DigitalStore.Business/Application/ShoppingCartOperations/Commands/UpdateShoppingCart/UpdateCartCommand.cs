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

namespace DigitalStore.Business.Application.ShoppingCartOperations.Commands.UpdateShoppingCart
{
    public record UpdateCartCommand(long shoppingCartId, ShoppingCartRequest Request) : IRequest<ApiResponse>;
    public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateCartCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.ShoppingCartRepository.GetById(request.shoppingCartId);
            if (entity == null)
            {
                return new ApiResponse("No related cart found.");
            }

            // Map the request to the existing entity
            mapper.Map(request.Request, entity);
            unitOfWork.ShoppingCartRepository.Update(entity);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
