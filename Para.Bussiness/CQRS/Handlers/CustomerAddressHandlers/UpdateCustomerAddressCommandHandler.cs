using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.CQRS.Commands.CustomerAddressCommands;
using Para.Data.UnitOfWork;


namespace Para.Bussiness.CQRS.Handlers.CustomerAddressHandlers
{
    public class UpdateCustomerAddressCommandHandler : IRequestHandler<UpdateCustomerAddressCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCustomerAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var customerAddress = await _unitOfWork.CustomerAddressRepository.GetById(request.customerAddresId);
            if (customerAddress == null)
            {
                return new ApiResponse("Customer Address not found.");
            }

            var mapped = _mapper.Map(request.Request, customerAddress);
            _unitOfWork.CustomerAddressRepository.Update(mapped);
            await _unitOfWork.Complete();

            return new ApiResponse();

        }
    }
}
