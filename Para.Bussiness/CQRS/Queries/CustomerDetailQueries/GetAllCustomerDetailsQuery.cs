using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.CQRS.Queries.CustomerDetailQueries
{
    public record GetAllCustomerDetailsQuery() : IRequest<ApiResponse<List<CustomerDetailResponse>>>
    {
    }
}
