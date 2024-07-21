using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.CQRS.Queries.CustomerQueries
{
    public record GetCustomerByIdQuery(long CustomerId) : IRequest<ApiResponse<CustomerResponse>>
    {
    }
}
