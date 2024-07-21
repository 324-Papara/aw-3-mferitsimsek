using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.CQRS.Queries.CustomerAddressQueries
{
    public record GetAllCustomerAddressQuery : IRequest<ApiResponse<List<CustomerAddressResponse>>>
    {
    }
}
