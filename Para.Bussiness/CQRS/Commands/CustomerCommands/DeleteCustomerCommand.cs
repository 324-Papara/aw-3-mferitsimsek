using MediatR;
using Para.Base.Response;

namespace Para.Bussiness.CQRS.Commands.CustomerCommands
{
    public record DeleteCustomerCommand(long CustomerId) : IRequest<ApiResponse>
    {
    }
}
