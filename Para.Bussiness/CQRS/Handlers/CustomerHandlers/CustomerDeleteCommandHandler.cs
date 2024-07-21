using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.CQRS.Commands.CustomerCommands;
using Para.Data.UnitOfWork;

namespace Para.Bussiness.CQRS.Handlers.CustomerHandlers
{
    public class CustomerDeleteCommandHandler : IRequestHandler<DeleteCustomerCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerDeleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {

            await _unitOfWork.CustomerRepository.Delete(request.CustomerId);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
