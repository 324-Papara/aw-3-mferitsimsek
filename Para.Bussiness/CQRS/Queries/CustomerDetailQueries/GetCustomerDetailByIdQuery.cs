using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.CQRS.Queries.CustomerDetailQueries
{
    public record GetCustomerDetailByIdQuery(long customerDetailId) : IRequest<ApiResponse<CustomerDetailResponse>>
    {
    }
}
