using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Bussiness.CQRS.Commands.CustomerAddressCommands;
using Para.Bussiness.CQRS.Queries.CustomerAddressQueries;
using Para.Schema;

namespace Para.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerAddressController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerAddressController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ApiResponse<List<CustomerAddressResponse>>> Get()
        {
            var operation = new GetAllCustomerAddressQuery();
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerAddressId}")]
        public async Task<ApiResponse<CustomerAddressResponse>> Get([FromRoute] long customerAddressId)
        {
            var operation = new GetCustomerAddressByIdQuery(customerAddressId);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerId}")]
        public async Task<ApiResponse<CustomerAddressResponse>> GetByCustomerId([FromRoute] long customerId)
        {
            var operation = new GetCustomerAddressByCustomerIdQuery(customerId);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerAddressResponse>> Post([FromBody] CustomerAddressRequest value)
        {
            var operation = new CreateCustomerAddressCommand(value);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPut("{customerAddressId}")]
        public async Task<ApiResponse> Put(long customerAddressId, [FromBody] CustomerAddressRequest value)
        {
            var operation = new UpdateCustomerAddressCommand(customerAddressId, value);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpDelete("{customerAddressId}")]
        public async Task<ApiResponse> Delete(long customerAddressId)
        {
            var operation = new DeleteCustomerAddressCommand(customerAddressId);
            var result = await _mediator.Send(operation);
            return result;
        }
    }
}
