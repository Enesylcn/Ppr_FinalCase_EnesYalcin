using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Data.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.ProductOperations.Commands.DeleteProduct
{
    public record DeleteProductCommand(long ProductId) : IRequest<ApiResponse>;
    public class DeleteProductCommandHandler
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.ProductRepository.Delete(request.ProductId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
