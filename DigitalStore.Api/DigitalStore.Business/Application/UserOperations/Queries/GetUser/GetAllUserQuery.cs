using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Business.Application.UserOperations.Commands.UpdateUser;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.UserOperations.Queries.GetUser
{
    public record GetAllUserQuery() : IRequest<ApiResponse<List<UserResponse>>>;

    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, ApiResponse<List<UserResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public GetAllUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<UserResponse>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            List<User> entityList = await unitOfWork.UserRepository.GetAll("User");
            var mappedList = mapper.Map<List<UserResponse>>(entityList);
            return new ApiResponse<List<UserResponse>>(mappedList);
        }
    }
}
