namespace Para.Schema.DTOs
{
    public class CustomerReportDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public int CustomerNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public CustomerDetailDto CustomerDetail { get; set; }
        public List<CustomerAddressDto> CustomerAddresses { get; set; }
        public List<CustomerPhoneDto> CustomerPhones { get; set; }
    }
}
