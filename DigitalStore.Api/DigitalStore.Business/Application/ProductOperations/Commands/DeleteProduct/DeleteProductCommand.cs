using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Business.Application.ProductOperations.Commands.CreateProduct;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.ProductOperations.Commands.DeleteProduct
{
    public record DeleteProductCommand(long ProductId) : IRequest<ApiResponse>;
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ApiResponse>
    {
        private readonly IUnitOfWork<Product> unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork<Product> unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.GenericRepository.Delete(request.ProductId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
