using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Business.Application.CategoryOperations.Queries.GetCategoryDetails;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.OrderDetailOperations.Commands.CreateOrderDetail
{
    public record CreateOrderDetailCommand(OrderDetailRequest Request) : IRequest<ApiResponse<OrderDetailResponse>>;
    public class CreateOrderDetailCommandHandler : IRequestHandler<CreateOrderDetailCommand, ApiResponse<OrderDetailResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateOrderDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<OrderDetailResponse>> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<OrderDetailRequest, OrderDetail>(request.Request);
            await unitOfWork.OrderDetailRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<OrderDetailResponse>(mapped);
            return new ApiResponse<OrderDetailResponse>(response);
        }
    }
}
