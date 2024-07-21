using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.CQRS.Commands.CustomerDetailCommands
{
    public record UpdateCustomerDetailCommand(long customerDetailId, CustomerDetailRequest Request) : IRequest<ApiResponse>
    {
    }
}
