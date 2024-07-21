using MediatR;
using Para.Base.Response;
using Para.Bussiness.CQRS.Commands.CustomerDetailCommands;
using Para.Data.UnitOfWork;

namespace Para.Bussiness.CQRS.Handlers.CustomerDetailHandlers
{
    public class DeleteCustomerDetailCommandHandler : IRequestHandler<DeleteCustomerDetailCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCustomerDetailCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse> Handle(DeleteCustomerDetailCommand request, CancellationToken cancellationToken)
        {
            var customerDetail = await _unitOfWork.CustomerDetailRepository.GetById(request.customerDetailId);
            if (customerDetail == null)
            {
                return new ApiResponse("Customer Detail not found.");
            }
            await _unitOfWork.CustomerDetailRepository.Delete(request.customerDetailId);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
