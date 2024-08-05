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

namespace DigitalStore.Business.Application.CategoryOperations.Queries.GetCategory
{
    public record GetAllCategoryQuery() : IRequest<ApiResponse<List<CategoryResponse>>>;

    public class GetCategoriesQueryHandler
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public GetCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<CategoryResponse>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            //List<Category> entityList = await unitOfWork.CategoryRepository.GetAll("CategoryDetail", "CategoryAddresses", "CategoryPhones"); Tırnak içinde yazılan alarlar ile o modele ait responsa ilgiler eklenir.

            List<Category> entityList = await unitOfWork.CategoryRepository.GetAll("Category");
            var mappedList = mapper.Map<List<CategoryResponse>>(entityList);
            return new ApiResponse<List<CategoryResponse>>(mappedList);
        }
    }
}
