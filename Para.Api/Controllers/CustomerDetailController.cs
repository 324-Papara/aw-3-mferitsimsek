﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Bussiness.CQRS.Commands.CustomerDetailCommands;
using Para.Bussiness.CQRS.Queries.CustomerDetailQueries;
using Para.Schema;

namespace Para.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerDetailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerDetailController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ApiResponse<List<CustomerDetailResponse>>> Get()
        {
            var operation = new GetAllCustomerDetailsQuery();
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerDetailId}")]
        public async Task<ApiResponse<CustomerDetailResponse>> Get([FromRoute] long customerDetailId)
        {
            var operation = new GetCustomerDetailByIdQuery(customerDetailId);
            var result = await _mediator.Send(operation);
            return result;
        }
        [HttpGet("{customerId}")]
        public async Task<ApiResponse<CustomerDetailResponse>> GetByCustomerId([FromRoute] long customerId)
        {
            var operation = new GetCustomerDetailByCustomerIdQuery(customerId);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerDetailResponse>> Post([FromBody] CustomerDetailRequest value)
        {
            var operation = new CreateCustomerDetailCommand(value);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPut("{customerDetailId}")]
        public async Task<ApiResponse> Put(long customerDetailId, [FromBody] CustomerDetailRequest value)
        {
            var operation = new UpdateCustomerDetailCommand(customerDetailId, value);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpDelete("{customerDetailId}")]
        public async Task<ApiResponse> Delete(long customerDetailId)
        {
            var operation = new DeleteCustomerDetailCommand(customerDetailId);
            var result = await _mediator.Send(operation);
            return result;
        }
    }
}
