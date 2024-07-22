using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.CQRS.Commands.CustomerDetailCommands;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.CQRS.Handlers.CustomerDetailHandlers
{
    public class CreateCustomerDetailCommandHandler : IRequestHandler<CreateCustomerDetailCommand, ApiResponse<CustomerDetailResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCustomerDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CustomerDetailResponse>> Handle(CreateCustomerDetailCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetWithWhereAndInclude(c=>c.Id==request.Request.CustomerId,
                                                                               c=> c.CustomerDetail);
            if (customer==null)
            {
                return new ApiResponse<CustomerDetailResponse>("Customer not found.");
            }

            if (customer.CustomerDetail!=null)
            {
                return new ApiResponse<CustomerDetailResponse>("Customer already has a detail.");
            }

            var mapped = _mapper.Map<CustomerDetailRequest, CustomerDetail>(request.Request);
            await _unitOfWork.CustomerDetailRepository.Insert(mapped);
            await _unitOfWork.Complete();

            var response = _mapper.Map<CustomerDetailResponse>(mapped);
            return new ApiResponse<CustomerDetailResponse>(response);
        }
    }
}
