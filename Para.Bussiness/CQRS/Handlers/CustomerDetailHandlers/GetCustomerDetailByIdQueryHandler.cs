﻿using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.CQRS.Queries.CustomerDetailQueries;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.CQRS.Handlers.CustomerDetailHandlers
{
    public class GetCustomerDetailByIdQueryHandler : IRequestHandler<GetCustomerDetailByIdQuery, ApiResponse<CustomerDetailResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCustomerDetailByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse<CustomerDetailResponse>> Handle(GetCustomerDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var customerDetail = await _unitOfWork.CustomerDetailRepository.GetWithWhereAndInclude(
                c => c.Id == request.customerDetailId,
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
