using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Business.Application.OrderDetailOperations.Commands.UpdateOrderDetail;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.OrderDetailOperations.Queries.GetOrderDetail
{
    public record GetAllOrderDetailQuery() : IRequest<ApiResponse<List<OrderDetailResponse>>>;

    public class GetAllOrderDetailQueryHandler : IRequestHandler<GetAllOrderDetailQuery, ApiResponse<List<OrderDetailResponse>>>
    {
        private readonly IUnitOfWork<OrderDetail> unitOfWork;
        private readonly IMapper mapper;
        public GetAllOrderDetailQueryHandler(IUnitOfWork<OrderDetail> unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<OrderDetailResponse>>> Handle(GetAllOrderDetailQuery request, CancellationToken cancellationToken)
        {
            List<OrderDetail> entityList = await unitOfWork.GenericRepository.GetAll("OrderDetail");
            var mappedList = mapper.Map<List<OrderDetailResponse>>(entityList);
            return new ApiResponse<List<OrderDetailResponse>>(mappedList);
        }
    }
}
