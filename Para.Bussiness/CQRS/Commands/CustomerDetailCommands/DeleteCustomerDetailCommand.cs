using MediatR;
using Para.Base.Response;

namespace Para.Bussiness.CQRS.Commands.CustomerDetailCommands
{
    public record DeleteCustomerDetailCommand(long customerDetailId) : IRequest<ApiResponse>
    {
    }
}
