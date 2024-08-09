﻿using AutoMapper;
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

namespace DigitalStore.Business.Application.ShoppingCartOperations.Commands.CreateShoppingCart
{
    public record CreateCartCommand(ShoppingCartRequest Request) : IRequest<ApiResponse<ShoppingCartResponse>>;

    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, ApiResponse<ShoppingCartResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateCartCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<ShoppingCartResponse>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<ShoppingCartRequest, ShoppingCart>(request.Request);
            await unitOfWork.ShoppingCartRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<ShoppingCartResponse>(mapped);
            return new ApiResponse<ShoppingCartResponse>(response);
        }
    }
}
