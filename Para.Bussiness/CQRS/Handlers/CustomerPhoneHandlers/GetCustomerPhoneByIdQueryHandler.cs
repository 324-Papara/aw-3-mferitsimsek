using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.CQRS.Queries.CustomerAddressQueries;
using Para.Bussiness.CQRS.Queries.CustomerPhoneQueries;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.CQRS.Handlers.CustomerPhoneHandlers
{
    public class GetCustomerPhoneByIdQueryHandler : IRequestHandler<GetCustomerPhoneByIdQuery, ApiResponse<CustomerPhoneResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCustomerPhoneByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CustomerPhoneResponse>> Handle(GetCustomerPhoneByIdQuery request, CancellationToken cancellationToken)
        {
            var customerPhone = await _unitOfWork.CustomerPhoneRepository.GetWithInclude(c => c.Id == request.customerPhoneId,
            c => c.Customer);

            if (customerPhone == null)
                return new ApiResponse<CustomerPhoneResponse>("Customer Phone not found");

            var mapped = _mapper.Map<CustomerPhoneResponse>(customerPhone);
            return new ApiResponse<CustomerPhoneResponse>(mapped);
        }
    }
}
