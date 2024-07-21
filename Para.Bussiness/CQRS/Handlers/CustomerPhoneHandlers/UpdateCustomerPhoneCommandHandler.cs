using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.CQRS.Commands.CustomerPhoneCommands;
using Para.Data.UnitOfWork;

namespace Para.Bussiness.CQRS.Handlers.CustomerPhoneHandlers
{
    public class UpdateCustomerPhoneCommandHandler : IRequestHandler<UpdateCustomerPhoneCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCustomerPhoneCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(UpdateCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            var customerPhone = await _unitOfWork.CustomerPhoneRepository.GetById(request.customerPhoneId);
            if (customerPhone == null)
            {
                return new ApiResponse("Customer Phone not found.");
            }
            var mapped = _mapper.Map(request.Request, customerPhone);
            _unitOfWork.CustomerPhoneRepository.Update(mapped);
            await _unitOfWork.Complete();

            return new ApiResponse();

        }
    }
}
