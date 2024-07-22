using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.CQRS.Queries.CustomerPhoneQueries;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.CQRS.Handlers.CustomerPhoneHandlers
{
    public class GetCustomerPhoneByCustomerIdQueryHandler : IRequestHandler<GetCustomerPhoneByCustomerIdQuery, ApiResponse<CustomerPhoneResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCustomerPhoneByCustomerIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CustomerPhoneResponse>> Handle(GetCustomerPhoneByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var customerPhone = await _unitOfWork.CustomerPhoneRepository.GetAllWithWhereAndInclude(c => c.CustomerId == request.customerId,
            c => c.Customer);

            if (customerPhone == null)
                return new ApiResponse<CustomerPhoneResponse>("Customer Phone not found");

            var mapped = _mapper.Map<List<CustomerPhoneResponse>>(customerPhone);
            return new ApiResponse<CustomerPhoneResponse>(mapped);
        }
    }
}
