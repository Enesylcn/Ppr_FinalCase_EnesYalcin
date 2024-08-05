using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Data.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.OrderOperations.Commands.DeleteOrder
{
    public record DeleteOrderCommand(long OrderId) : IRequest<ApiResponse>;
    public class DeleteOrderCommandHandler
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.OrderRepository.Delete(request.OrderId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
