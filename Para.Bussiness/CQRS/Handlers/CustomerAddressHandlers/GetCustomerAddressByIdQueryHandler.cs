using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.CQRS.Queries.CustomerAddressQueries;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.CQRS.Handlers.CustomerAddressHandlers
{
    public class GetCustomerAddressByIdQueryHandler : IRequestHandler<GetCustomerAddressByIdQuery, ApiResponse<CustomerAddressResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCustomerAddressByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse<CustomerAddressResponse>> Handle(GetCustomerAddressByIdQuery request, CancellationToken cancellationToken)
        {
            var customerAdress = await _unitOfWork.CustomerAddressRepository.GetWithInclude(
                c => c.Id == request.customerAddressId,
                c => c.Customer);

            if (customerAdress == null)
                return new ApiResponse<CustomerAddressResponse>("Customer Address not found");

            var mapped = _mapper.Map<CustomerAddressResponse>(customerAdress);
            return new ApiResponse<CustomerAddressResponse>(mapped);
        }
    }
}
