using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.CQRS.Queries.CustomerDetailQueries;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.CQRS.Handlers.CustomerDetailHandlers
{
    public class GetAllCustomerDetailsQueryHandler : IRequestHandler<GetAllCustomerDetailsQuery, ApiResponse<List<CustomerDetailResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCustomerDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse<List<CustomerDetailResponse>>> Handle(GetAllCustomerDetailsQuery request, CancellationToken cancellationToken)
        {
            List<CustomerDetail> customerDetailList = await _unitOfWork.CustomerDetailRepository.GetAll(); 
            var mapped = _mapper.Map<List<CustomerDetailResponse>>(customerDetailList);
            return new ApiResponse<List<CustomerDetailResponse>>(mapped);
        }
    }
}
