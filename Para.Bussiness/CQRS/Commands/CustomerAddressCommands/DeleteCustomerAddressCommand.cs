using MediatR;
using Para.Base.Response;


namespace Para.Bussiness.CQRS.Commands.CustomerAddressCommands
{
    public record DeleteCustomerAddressCommand(long customerAddresId) : IRequest<ApiResponse>
    {
    }
}
