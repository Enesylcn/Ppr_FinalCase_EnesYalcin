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

namespace DigitalStore.Business.Application.CouponOperations.Commands.UpdateCoupon
{
    public record UpdateCouponCommand(long CouponId, CouponRequest Request) : IRequest<ApiResponse>;
    public class UpdateCouponCommandHandler : IRequestHandler<UpdateCouponCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateCouponCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.CouponRepository.GetById(request.CouponId);
            if (entity == null)
            {
                return new ApiResponse("No related Coupon found.");
            }

            mapper.Map(request.Request, entity);
            unitOfWork.CouponRepository.Update(entity);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
