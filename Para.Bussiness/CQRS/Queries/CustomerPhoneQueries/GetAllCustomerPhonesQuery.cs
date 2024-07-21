using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.CQRS.Queries.CustomerPhoneQueries
{
    public record GetAllCustomerPhonesQuery() : IRequest<ApiResponse<List<CustomerPhoneResponse>>>
    {
    }
}
