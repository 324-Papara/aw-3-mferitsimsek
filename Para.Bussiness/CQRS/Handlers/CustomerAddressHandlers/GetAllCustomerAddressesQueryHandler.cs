using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.CQRS.Queries.CustomerAddressQueries;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.CQRS.Handlers.CustomerAddressHandlers
{
    public class GetAllCustomerAddressesQueryHandler : IRequestHandler<GetAllCustomerAddressQuery, ApiResponse<List<CustomerAddressResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCustomerAddressesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse<List<CustomerAddressResponse>>> Handle(GetAllCustomerAddressQuery request, CancellationToken cancellationToken)
        {
            List<CustomerAddress> customerAddresses = await _unitOfWork.CustomerAddressRepository.GetAll();
            var mapped = _mapper.Map<List<CustomerAddressResponse>>(customerAddresses);
            return new ApiResponse<List<CustomerAddressResponse>>(mapped);
        }
    }
}
