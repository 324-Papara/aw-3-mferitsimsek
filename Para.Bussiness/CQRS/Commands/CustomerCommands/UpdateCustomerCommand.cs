using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.CQRS.Commands.CustomerCommands
{
    public record UpdateCustomerCommand(long CustomerId, CustomerUpdateRequest Request) : IRequest<ApiResponse>
    {
    }
}
