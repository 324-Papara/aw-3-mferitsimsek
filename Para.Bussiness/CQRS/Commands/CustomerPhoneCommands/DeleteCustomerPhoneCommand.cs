using MediatR;
using Para.Base.Response;


namespace Para.Bussiness.CQRS.Commands.CustomerPhoneCommands
{
    public record DeleteCustomerPhoneCommand(long customerPhoneId) : IRequest<ApiResponse>
    {
    }
}
