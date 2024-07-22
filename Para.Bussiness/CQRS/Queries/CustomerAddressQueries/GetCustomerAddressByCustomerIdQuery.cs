using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.CQRS.Queries.CustomerAddressQueries
{
    public record GetCustomerAddressByCustomerIdQuery(long customerId) : IRequest<ApiResponse<CustomerAddressResponse>>
    {
    }
}
