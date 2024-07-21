using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.CQRS.Commands.CustomerDetailCommands;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.CQRS.Handlers.CustomerDetailHandlers
{
    public class UpdateCustomerDetailCommandHandler : IRequestHandler<UpdateCustomerDetailCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCustomerDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(UpdateCustomerDetailCommand request, CancellationToken cancellationToken)
        {
            var customerDetail = await _unitOfWork.CustomerDetailRepository.GetById(request.customerDetailId);
            if (customerDetail == null)
            {
                return new ApiResponse("Customer Detail not found.");
            }
           
            var mapped = _mapper.Map(request.Request, customerDetail);
            _unitOfWork.CustomerDetailRepository.Update(mapped);
            await _unitOfWork.Complete();

            return new ApiResponse();
        }
    }
}
