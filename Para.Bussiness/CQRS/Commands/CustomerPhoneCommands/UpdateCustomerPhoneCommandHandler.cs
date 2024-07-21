using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.CQRS.Commands.CustomerPhoneCommands
{
    public record UpdateCustomerPhoneCommand(long customerPhoneId, CustomerPhoneRequest Request) : IRequest<ApiResponse>
    {
    }
}
