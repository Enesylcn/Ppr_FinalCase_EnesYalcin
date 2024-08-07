using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Business.Application.OrderOperations.Commands.CreateOrder;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.OrderOperations.Commands.DeleteOrder
{
    public record DeleteOrderCommand(long OrderId) : IRequest<ApiResponse>;
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, ApiResponse>
    {
        private readonly IUnitOfWork<Order> unitOfWork;

        public DeleteOrderCommandHandler(IUnitOfWork<Order> unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.GenericRepository.Delete(request.OrderId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
