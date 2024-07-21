using MediatR;
using Para.Base.Response;
using Para.Bussiness.CQRS.Commands.CustomerPhoneCommands;
using Para.Data.UnitOfWork;

namespace Para.Bussiness.CQRS.Handlers.CustomerPhoneHandlers
{
    public class DeleteCustomerPhoneCommandHandler : IRequestHandler<DeleteCustomerPhoneCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCustomerPhoneCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse> Handle(DeleteCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            var customerPhone = await _unitOfWork.CustomerPhoneRepository.GetById(request.customerPhoneId);
            if (customerPhone == null)
            {
                return new ApiResponse("Customer Phone not found.");
            }

            await _unitOfWork.CustomerPhoneRepository.Delete(request.customerPhoneId);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
