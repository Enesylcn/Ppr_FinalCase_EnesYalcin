using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;

namespace DigitalStore.Business.Application.CouponOperations.Commands.CreateCoupon
{
    public record CreateCouponCommand(CouponRequest Request) : IRequest<ApiResponse<CouponResponse>>;

    public class CreateCouponCommandHandler : IRequestHandler<CreateCouponCommand, ApiResponse<CouponResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateCouponCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<CouponResponse>> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<CouponRequest, Coupon>(request.Request);
            mapped.Name = $"{mapped.Code}_{mapped.ValidUntil}";
            await unitOfWork.CouponRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<CouponResponse>(mapped);
            return new ApiResponse<CouponResponse>(response);
        }
    }
}
