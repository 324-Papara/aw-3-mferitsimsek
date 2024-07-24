using Microsoft.AspNetCore.Mvc;
using Para.Data.Domain;
using Para.Data.GenericRepository;
using Para.Schema.DTOs;

[ApiController]
[Route("api/[controller]")]
public class CustomerReportController : ControllerBase
{
    private readonly IGenericRepository<Customer> _repository;

    public CustomerReportController(IGenericRepository<Customer> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerReportDto>>> GetCustomerReports()
    {
        var query = @"
            SELECT c.*, cd.*, ca.*, cp.*
            FROM Customer c
            LEFT JOIN CustomerDetail cd ON c.Id = cd.CustomerId
            LEFT JOIN CustomerAddress ca ON c.Id = ca.CustomerId
            LEFT JOIN CustomerPhone cp ON c.Id = cp.CustomerId";

        var result = await _repository.GetCustomReportAsync<CustomerReportDto>(query);

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

        var result = await _repository.GetCustomReportAsync<CustomerReportDto>(query, new { CustomerId = customerId });

        var customerReport = result.FirstOrDefault();

        if (customerReport == null)
        {
            return NotFound();
        }

        return Ok(customerReport);
    }
}