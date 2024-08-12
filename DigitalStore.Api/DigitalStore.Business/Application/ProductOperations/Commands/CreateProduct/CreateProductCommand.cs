using AutoMapper;
using DigitalStore.Base;
using DigitalStore.Base.Response;
using DigitalStore.Business.Application.OrderOperations.Queries.GetOrder;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.ProductOperations.Commands.CreateProduct
{
    public record CreateProductCommand(ProductRequest Request) : IRequest<ApiResponse<ProductResponse>>;

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResponse<ProductResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ISessionContext sessionContext;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionContext sessionContext)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.sessionContext = sessionContext;
        }

        public async Task<ApiResponse<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // ProductRequest nesnesinden Product nesnesini oluştur
            var product = mapper.Map<Product>(request.Request);

            // Kategori ID'lerini işleyin ve ilişkileri kurun
            if (request.Request.CategoryIds != null && request.Request.CategoryIds.Any())
            {
                // CategoryRepository'den ilgili kategorileri getirin
                var categories = await unitOfWork.CategoryRepository.Where(c => request.Request.CategoryIds.Contains(c.Id));

                // ProductCategories ilişkilerini kurun
                product.ProductCategories = categories.Select(c => new ProductCategory
                {
                    ProductId = product.Id,
                    CategoryId = c.Id,
                    InsertUser = sessionContext.Session.UserName,
                    Name = c.Name

                }).ToList();
            }

            // Ürünü veritabanına ekleyin
            await unitOfWork.ProductRepository.Insert(product);
            await unitOfWork.Complete();

            // Response nesnesini oluştur ve döndür
            var response = mapper.Map<ProductResponse>(product);
            return new ApiResponse<ProductResponse>(response);
        }
    }
}
