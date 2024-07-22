using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.CQRS.Commands.CustomerAddressCommands;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.CQRS.Handlers.CustomerAddressHandlers
{
    public class CreateCustomerAddressCommandHandler : IRequestHandler<CreateCustomerAddressCommand, ApiResponse<CustomerAddressResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCustomerAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse<CustomerAddressResponse>> Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            // Eğer her kullanıcının tek adresi olacaksa aşağıdaki kod devreye alınabilir. İş kuralı gereği birden fazla adresi olabilir.
            #region Tek adress kontrolü
            //var customer = await _unitOfWork.CustomerRepository.GetWithWhereAndInclude(c=> c.Id==request.Request.CustomerId,
            //                                                                   c => c.CustomerAddresses);
            //if (customer == null)
            //{
            //    return new ApiResponse<CustomerAddressResponse>("Customer not found.");
            //}

            //if (customer.CustomerAddresses == null)
            //{
            //    return new ApiResponse<CustomerAddressResponse>("Customer already has a address.");
            //}

            #endregion

            var mapped = _mapper.Map<CustomerAddressRequest, CustomerAddress>(request.Request);
            await _unitOfWork.CustomerAddressRepository.Insert(mapped);
            await _unitOfWork.Complete();

            var response = _mapper.Map<CustomerAddressResponse>(mapped);
            return new ApiResponse<CustomerAddressResponse>(response);
        }
    }
}
