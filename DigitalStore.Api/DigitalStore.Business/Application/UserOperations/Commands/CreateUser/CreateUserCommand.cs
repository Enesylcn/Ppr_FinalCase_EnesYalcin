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

namespace DigitalStore.Business.Application.UserOperations.Commands.CreateUser
{
    public record CreateUserCommand(UserRequest Request) : IRequest<ApiResponse<UserResponse>>;

    public class CreateUserCommandHandler
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<UserRequest, User>(request.Request);
            await unitOfWork.UserRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<UserResponse>(mapped);
            return new ApiResponse<UserResponse>(response);
        }
    }
}
