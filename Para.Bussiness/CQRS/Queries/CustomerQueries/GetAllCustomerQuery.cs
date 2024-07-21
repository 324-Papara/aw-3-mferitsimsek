using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.CQRS.Queries.CustomerQueries
{
    public record GetAllCustomerQuery() : IRequest<ApiResponse<List<CustomerResponse>>>
    {
    }
}
