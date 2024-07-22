using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Bussiness.CQRS.Commands.CustomerPhoneCommands;
using Para.Bussiness.CQRS.Queries.CustomerPhoneQueries;
using Para.Data.Domain;
using Para.Schema;

namespace Para.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerPhoneController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerPhoneController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ApiResponse<List<CustomerPhoneResponse>>> Get()
        {
            var operation = new GetAllCustomerPhonesQuery();
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerPhoneId}")]
        public async Task<ApiResponse<CustomerPhoneResponse>> Get([FromRoute] long customerPhoneId)
        {
            var operation = new GetCustomerPhoneByIdQuery(customerPhoneId);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerId}")]
        public async Task<ApiResponse<CustomerPhoneResponse>> GetByCustomerId([FromRoute] long customerId)
        {
            var operation = new GetCustomerPhoneByCustomerIdQuery(customerId);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerPhoneResponse>> Post([FromBody] CustomerPhoneRequest value)
        {
            var operation = new CreateCustomerPhoneCommand(value);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPut("{customerPhoneId}")]
        public async Task<ApiResponse> Put(long customerPhoneId, [FromBody] CustomerPhoneRequest value)
        {
            var operation = new UpdateCustomerPhoneCommand(customerPhoneId, value);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpDelete("{customerPhoneId}")]
        public async Task<ApiResponse> Delete(long customerPhoneId)
        {
            var operation = new DeleteCustomerPhoneCommand(customerPhoneId);
            var result = await _mediator.Send(operation);
            return result;
        }
    }
}
