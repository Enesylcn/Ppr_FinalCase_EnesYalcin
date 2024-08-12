using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Data.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.CouponOperations.Commands.DeleteCoupon
{
    public record DeleteCouponCommand(long CouponId) : IRequest<ApiResponse>;
    public class DeleteCouponCommandHandler : IRequestHandler<DeleteCouponCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteCouponCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.CouponRepository.Delete(request.CouponId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
