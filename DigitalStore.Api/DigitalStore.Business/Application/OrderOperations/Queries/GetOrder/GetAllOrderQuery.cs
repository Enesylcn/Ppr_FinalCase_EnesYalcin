using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Business.Application.OrderOperations.Commands.UpdateOrder;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.OrderOperations.Queries.GetOrder
{
    public record GetAllOrderQuery() : IRequest<ApiResponse<List<OrderResponse>>>;

    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, ApiResponse<List<OrderResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public GetAllOrderQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<OrderResponse>>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            List<Order> entityList = await unitOfWork.OrderRepository.GetAll();
            var mappedList = mapper.Map<List<OrderResponse>>(entityList);
            return new ApiResponse<List<OrderResponse>>(mappedList);
        }
    }
}
