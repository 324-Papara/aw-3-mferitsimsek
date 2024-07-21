using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.CQRS.Commands.CustomerPhoneCommands;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.CQRS.Handlers.CustomerPhoneHandlers
{
    public class CreateCustomerPhoneCommandHandler : IRequestHandler<CreateCustomerPhoneCommand, ApiResponse<CustomerPhoneResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCustomerPhoneCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CustomerPhoneResponse>> Handle(CreateCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<CustomerPhoneRequest, CustomerPhone>(request.Request);
            await _unitOfWork.CustomerPhoneRepository.Insert(mapped);
            await _unitOfWork.Complete();

            var response = _mapper.Map<CustomerPhoneResponse>(mapped);
            return new ApiResponse<CustomerPhoneResponse>(response);
        }
    }
}
