using AutoMapper;
using Para.Data.Domain;
using Para.Schema;

namespace Para.Bussiness;

public class MapperConfig : Profile
{

    public MapperConfig()
    {
        CreateMap<Customer, CustomerResponse>();
        CreateMap<CustomerRequest, Customer>().ForMember(dest => dest.CustomerNumber, opt => opt.Ignore());
        CreateMap<CustomerUpdateRequest, Customer>().ForMember(dest => dest.CustomerNumber, opt => opt.Ignore());
        
        CreateMap<CustomerAddress, CustomerAddressResponse>();
        CreateMap<CustomerAddressRequest, CustomerAddress>();
        
        CreateMap<CustomerPhone, CustomerPhoneResponse>().ReverseMap();
        CreateMap<CustomerPhoneRequest, CustomerPhone>();
        
        CreateMap<CustomerDetail, CustomerDetailResponse>();
        CreateMap<CustomerDetailRequest, CustomerDetail>();
    }
}