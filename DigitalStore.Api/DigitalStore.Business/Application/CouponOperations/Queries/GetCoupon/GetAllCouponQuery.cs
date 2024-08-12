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

namespace DigitalStore.Business.Application.CouponOperations.Queries.GetCoupon
{
    public record GetAllCouponQuery() : IRequest<ApiResponse<List<CouponResponse>>>;

    public class GetCategoriesQueryHandler : IRequestHandler<GetAllCouponQuery, ApiResponse<List<CouponResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public GetCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<CouponResponse>>> Handle(GetAllCouponQuery request, CancellationToken cancellationToken)
        {
            List<Coupon> entityList = await unitOfWork.CouponRepository.GetAll();
            var mappedList = mapper.Map<List<CouponResponse>>(entityList);
            return new ApiResponse<List<CouponResponse>>(mappedList);
        }
    }
}
