using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;

namespace DigitalStore.Business.Application.UserOperations.Commands.UpdateUser
{
    public record UpdateUserCommand(string UserId, UserRequest Request) : IRequest<ApiResponse>;
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<UserRequest, User>(request.Request);
            mapped.Id = request.UserId;
            unitOfWork.UserRepository.Update(mapped);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
