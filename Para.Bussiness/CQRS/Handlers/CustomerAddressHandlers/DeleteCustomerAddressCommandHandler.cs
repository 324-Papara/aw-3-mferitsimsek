using MediatR;
using Para.Base.Response;
using Para.Bussiness.CQRS.Commands.CustomerAddressCommands;
using Para.Data.UnitOfWork;

namespace Para.Bussiness.CQRS.Handlers.CustomerAddressHandlers
{
    public class DeleteCustomerAddressCommandHandler : IRequestHandler<DeleteCustomerAddressCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCustomerAddressCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var customerAddress = await _unitOfWork.CustomerAddressRepository.GetById(request.customerAddresId);
            if (customerAddress == null)
            {
                return new ApiResponse("Customer Address not found.");
            }

            await _unitOfWork.CustomerAddressRepository.Delete(request.customerAddresId);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
