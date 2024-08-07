﻿using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Business.Application.OrderOperations.Commands.DeleteOrder;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.OrderOperations.Commands.UpdateOrder
{
    public record UpdateOrderCommand(long OrderId, OrderRequest Request) : IRequest<ApiResponse>;
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<OrderRequest, Order>(request.Request);
            mapped.Id = request.OrderId;
            unitOfWork.OrderRepository.Update(mapped);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
