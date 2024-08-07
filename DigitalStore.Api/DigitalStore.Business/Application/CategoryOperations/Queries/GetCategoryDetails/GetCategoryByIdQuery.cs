using AutoMapper;
using DigitalStore.Base;
using DigitalStore.Base.Response;
using DigitalStore.Business.Application.CategoryOperations.Queries.GetCategory;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.CategoryOperations.Queries.GetCategoryDetails
{
    public record GetCategoryByIdQuery(long categoryId) : IRequest<ApiResponse<CategoryResponse>>;

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, ApiResponse<CategoryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ISessionContext sessionContext;

        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionContext sessionContext)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.sessionContext = sessionContext;
        }


        public async Task<ApiResponse<CategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.CategoryRepository.GetById(request.categoryId);
            var mapped = mapper.Map<CategoryResponse>(entity);
            return new ApiResponse<CategoryResponse>(mapped);
        }
    }
}
