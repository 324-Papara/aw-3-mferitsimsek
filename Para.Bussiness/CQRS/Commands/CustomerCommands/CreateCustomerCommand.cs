﻿using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.CQRS.Commands.CustomerCommands
{
    public record CreateCustomerCommand(CustomerRequest Request) : IRequest<ApiResponse<CustomerResponse>>
    {
    }
}
