using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.CQRS.Queries.CustomerQueries
{
    public record GetCustomerByParametersQuery(long CustomerId, string Name, string IdentityNumber) : IRequest<ApiResponse<List<CustomerResponse>>>
    {
    }
}
