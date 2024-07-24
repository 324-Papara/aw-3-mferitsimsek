using AutoMapper;
using Para.Data.Domain;
using Para.Schema;
using Para.Schema.DTOs;

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

        //Dapper

        CreateMap<Customer, CustomerReportDto>()
            .ForMember(dest => dest.CustomerDetail, opt => opt.MapFrom(src => src.CustomerDetail))
            .ForMember(dest => dest.CustomerAddresses, opt => opt.MapFrom(src => src.CustomerAddresses))
            .ForMember(dest => dest.CustomerPhones, opt => opt.MapFrom(src => src.CustomerPhones));

        CreateMap<CustomerDetail, CustomerDetailDto>();
        CreateMap<CustomerAddress, CustomerAddressDto>();
        CreateMap<CustomerPhone, CustomerPhoneDto>();
    }
}