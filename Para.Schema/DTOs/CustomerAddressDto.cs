﻿namespace Para.Schema.DTOs
{
    public class CustomerAddressDto
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string AddressLine { get; set; }
        public string ZipCode { get; set; }
        public bool IsDefault { get; set; }
    }
}
