using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.CQRS.Commands.CustomerAddressCommands
{
    public record CreateCustomerAddressCommand(CustomerAddressRequest Request):IRequest<ApiResponse<CustomerAddressResponse>>
    {
    }
}
