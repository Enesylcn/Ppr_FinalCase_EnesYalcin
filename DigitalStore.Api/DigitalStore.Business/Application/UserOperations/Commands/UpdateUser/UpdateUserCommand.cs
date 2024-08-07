using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Business.Application.UserOperations.Commands.DeleteUser;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.UserOperations.Commands.UpdateUser
{
    public record UpdateUserCommand(long UserId, UserRequest Request) : IRequest<ApiResponse>;
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResponse>
    {
        private readonly IUnitOfWork<User> unitOfWork;
        private readonly IMapper mapper;

        public UpdateUserCommandHandler(IUnitOfWork<User> unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<UserRequest, User>(request.Request);
            mapped.Id = request.UserId;
            unitOfWork.GenericRepository.Update(mapped);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
