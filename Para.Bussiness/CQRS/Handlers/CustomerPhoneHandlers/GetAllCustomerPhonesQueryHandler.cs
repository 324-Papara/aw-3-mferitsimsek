using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.CQRS.Queries.CustomerPhoneQueries;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.CQRS.Handlers.CustomerPhoneHandlers
{
    public class GetAllCustomerPhonesQueryHandler : IRequestHandler<GetAllCustomerPhonesQuery, ApiResponse<List<CustomerPhoneResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCustomerPhonesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<CustomerPhoneResponse>>> Handle(GetAllCustomerPhonesQuery request, CancellationToken cancellationToken)
        {
            List<CustomerPhone> customerPhones = await _unitOfWork.CustomerPhoneRepository.GetAll();
            var mapped = _mapper.Map<List<CustomerPhoneResponse>>(customerPhones);
            return new ApiResponse<List<CustomerPhoneResponse>>(mapped);
        }
    }
}
