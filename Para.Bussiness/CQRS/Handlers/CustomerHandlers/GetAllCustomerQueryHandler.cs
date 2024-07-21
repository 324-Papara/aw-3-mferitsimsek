using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.CQRS.Queries.CustomerQueries;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.CQRS.Handlers.CustomerHandlers
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, ApiResponse<List<CustomerResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCustomerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            List<Customer> customers = await _unitOfWork.CustomerRepository.GetAll();
            var mapped = _mapper.Map<List<CustomerResponse>>(customers);
            return new ApiResponse<List<CustomerResponse>>(mapped);
        }
    }
}
