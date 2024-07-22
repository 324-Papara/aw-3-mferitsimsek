using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.CQRS.Commands.CustomerCommands;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.CQRS.Handlers.CustomerHandlers
{
    public class CustomerUpdateCommandHandler : IRequestHandler<UpdateCustomerCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            // InsertUser InsertDate ve IsActive i değiştirmemek için eklendi. Bir diğer yöntem olarak MapperConfige örnekteki gibi eklenebilirler.
            // .ForMember(dest => dest.CustomerNumber, opt => opt.Ignore());

            var existingCustomer = await _unitOfWork.CustomerRepository.GetById(request.CustomerId);
            if (existingCustomer == null)
            {
                return new ApiResponse("Customer not found.");
            }
            var mapped = _mapper.Map(request.Request, existingCustomer);
            _unitOfWork.CustomerRepository.Update(mapped);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
