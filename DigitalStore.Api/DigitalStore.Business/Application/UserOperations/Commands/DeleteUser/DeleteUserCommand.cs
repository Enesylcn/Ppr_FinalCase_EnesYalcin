﻿using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Business.Application.UserOperations.Commands.CreateUser;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.UserOperations.Commands.DeleteUser
{
    public record DeleteUserCommand(long UserId) : IRequest<ApiResponse>;
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.UserRepository.Delete(request.UserId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
