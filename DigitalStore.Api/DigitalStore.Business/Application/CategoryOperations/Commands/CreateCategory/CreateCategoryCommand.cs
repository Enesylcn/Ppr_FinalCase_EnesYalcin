using AutoMapper;
using DigitalStore.Base;
using DigitalStore.Base.Response;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics.Metrics;

namespace DigitalStore.Business.Application.CategoryOperations.Commands.CreateCategory
{
    public record CreateCategoryCommand(CategoryRequest Request) : IRequest<ApiResponse<CategoryResponse>>;

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ApiResponse<CategoryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            //this.memoryCache = memoryCache;
            //this.distributedCache = distributedCache;
        }

        public async Task<ApiResponse<CategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<CategoryRequest, Category>(request.Request);
            await unitOfWork.CategoryRepository.Insert(mapped);
            await unitOfWork.Complete();

            //memoryCache.Remove("CategoryList");
            //await distributedCache.RemoveAsync("CategoryList");

            var response = mapper.Map<CategoryResponse>(mapped);
            return new ApiResponse<CategoryResponse>(response);
        }
    }

}
