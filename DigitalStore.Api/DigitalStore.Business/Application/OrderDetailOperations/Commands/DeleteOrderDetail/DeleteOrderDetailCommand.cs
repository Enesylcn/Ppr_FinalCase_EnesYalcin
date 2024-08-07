using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Business.Application.OrderDetailOperations.Commands.CreateOrderDetail;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.OrderDetailOperations.Commands.DeleteOrderDetail
{
    public record DeleteOrderDetailCommand(long OrderDetailId) : IRequest<ApiResponse>;
    public class DeleteOrderDetailCommandHandler : IRequestHandler<DeleteOrderDetailCommand, ApiResponse>
    {
        private readonly IUnitOfWork<OrderDetail> unitOfWork;

        public DeleteOrderDetailCommandHandler(IUnitOfWork<OrderDetail> unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse> Handle(DeleteOrderDetailCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.GenericRepository.Delete(request.OrderDetailId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
