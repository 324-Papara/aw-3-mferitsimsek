using Microsoft.AspNetCore.Mvc;
using Para.Data.Domain;
using Para.Data.GenericRepository;
using Para.Data.UnitOfWork;
using Para.Schema.DTOs;

[ApiController]
[Route("api/[controller]")]
public class CustomerReportController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerReportController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerReportDto>>> GetCustomerReports()
    {

        var query = @"
            SELECT 
                c.Id, c.FirstName, c.LastName, c.IdentityNumber, c.Email, c.CustomerNumber, c.DateOfBirth,
                cd.FatherName, cd.MotherName, cd.EducationStatus, cd.MontlyIncome, cd.Occupation,
                ca.Country, ca.City, ca.AddressLine, ca.ZipCode, ca.IsDefault AS AddressIsDefault,
                cp.CountyCode, cp.Phone, cp.IsDefault AS PhoneIsDefault
            FROM Customer c
            LEFT JOIN CustomerDetail cd ON c.Id = cd.CustomerId
            LEFT JOIN CustomerAddress ca ON c.Id = ca.CustomerId
            LEFT JOIN CustomerPhone cp ON c.Id = cp.CustomerId";
        var result = await _unitOfWork.CustomerRepository.GetCustomerReportAsync(query);

        if (result == null || !result.Any())
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("{customerId}")]
    public async Task<ActionResult<CustomerReportDto>> GetCustomerReport(long customerId)
    {
        var query = @"
            SELECT c.*, cd.*, ca.*, cp.*
            FROM Customer c
            LEFT JOIN CustomerDetail cd ON c.Id = cd.CustomerId
            LEFT JOIN CustomerAddress ca ON c.Id = ca.CustomerId
            LEFT JOIN CustomerPhone cp ON c.Id = cp.CustomerId
            WHERE c.Id = @CustomerId";

        var result = await _unitOfWork.CustomerRepository.GetCustomerReportAsync(query, new { CustomerId = customerId });

        var customerReport = result.FirstOrDefault();

        if (customerReport == null)
        {
            return NotFound();
        }

        return Ok(customerReport);
    }
}