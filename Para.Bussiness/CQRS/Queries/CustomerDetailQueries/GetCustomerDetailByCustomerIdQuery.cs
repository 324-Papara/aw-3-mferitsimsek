using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.CQRS.Queries.CustomerDetailQueries
{
    public record GetCustomerDetailByCustomerIdQuery(long customerId) : IRequest<ApiResponse<CustomerDetailResponse>>
    {
    }
}
