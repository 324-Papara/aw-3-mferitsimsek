using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.CQRS.Queries.CustomerDetailQueries;
using Para.Data.UnitOfWork;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.CQRS.Handlers.CustomerDetailHandlers
{
    public class GetCustomerDetailByCustomerIdQueryHandler : IRequestHandler<GetCustomerDetailByCustomerIdQuery, ApiResponse<CustomerDetailResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCustomerDetailByCustomerIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse<CustomerDetailResponse>> Handle(GetCustomerDetailByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var customerDetail = await _unitOfWork.CustomerDetailRepository.GetWithWhereAndInclude(
                c => c.CustomerId == request.customerId,
                c => c.Customer);


            if (customerDetail == null)
            {
                return new ApiResponse<CustomerDetailResponse>("Customer detail not found");
            }

            var mapped = _mapper.Map<CustomerDetailResponse>(customerDetail);
            return new ApiResponse<CustomerDetailResponse>(mapped);
        }
    }
}
