using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.CQRS.Commands.CustomerCommands;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.CQRS.Handlers.CustomerHandlers
{
    internal class CustomerCreateCommandHandler : IRequestHandler<CreateCustomerCommand, ApiResponse<CustomerResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse<CustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<CustomerRequest, Customer>(request.Request);
            mapped.CustomerNumber = new Random().Next(1000000, 9999999);
            await _unitOfWork.CustomerRepository.Insert(mapped);
            await _unitOfWork.Complete();

            var response = _mapper.Map<CustomerResponse>(mapped);
            return new ApiResponse<CustomerResponse>(response);
        }
    }
}
