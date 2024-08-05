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

namespace DigitalStore.Business.Application.OrderDetailOperations.Commands.UpdateOrderDetail
{
    public record UpdateOrderDetailCommand(long OrderDetailId, OrderDetailRequest Request) : IRequest<ApiResponse>;
    public class UpdateOrderDetailCommandHandler
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateOrderDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<OrderDetailRequest, OrderDetail>(request.Request);
            mapped.Id = request.OrderDetailId;
            unitOfWork.OrderDetailRepository.Update(mapped);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
