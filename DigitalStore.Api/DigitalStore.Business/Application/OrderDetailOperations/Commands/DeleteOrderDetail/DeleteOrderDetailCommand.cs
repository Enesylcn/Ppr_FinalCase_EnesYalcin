using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Data.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.OrderDetailOperations.Commands.DeleteOrderDetail
{
    public record DeleteOrderDetailCommand(long OrderDetailId) : IRequest<ApiResponse>;
    public class DeleteOrderDetailCommandHandler
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteOrderDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse> Handle(DeleteOrderDetailCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.OrderDetailRepository.Delete(request.OrderDetailId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
