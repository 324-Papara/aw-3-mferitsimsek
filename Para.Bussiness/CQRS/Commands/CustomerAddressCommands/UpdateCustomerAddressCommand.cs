using MediatR;
using Para.Base.Response;
using Para.Schema;


namespace Para.Bussiness.CQRS.Commands.CustomerAddressCommands
{
    public record UpdateCustomerAddressCommand(long customerAddresId, CustomerAddressRequest Request) : IRequest<ApiResponse>
    {
    }
}
