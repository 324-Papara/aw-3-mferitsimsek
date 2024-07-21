using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.CQRS.Queries.CustomerPhoneQueries
{
    public record GetCustomerPhoneByIdQuery(long customerPhoneId) : IRequest<ApiResponse<CustomerPhoneResponse>>
    {
    }
}
