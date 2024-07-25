using Microsoft.EntityFrameworkCore;
using Para.Base.Entity;
using Para.Data.Helpers;
using Para.Data.Context;
using System.Linq.Expressions;
using Para.Schema.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Humanizer.Configuration;
using Dapper;
using Para.Data.Domain;

namespace Para.Data.GenericRepository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ParaDbContext dbContext;

    public GenericRepository(ParaDbContext dbContext)
    {
        this.dbContext = dbContext;

    }

    public async Task Save()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task<TEntity?> GetById(long Id)
    {
        return await dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == Id);
    }

    public async Task Insert(TEntity entity)
    {
        entity.IsActive = true;
        entity.InsertDate = DateTime.UtcNow;
        entity.InsertUser = "System";
        await dbContext.Set<TEntity>().AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        dbContext.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        dbContext.Set<TEntity>().Remove(entity);
    }

    public async Task Delete(long Id)
    {
        var entity = await dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == Id);
        if (entity is not null)
            dbContext.Set<TEntity>().Remove(entity);
    }

    public async Task<List<TEntity>> GetAll()
    {
        return await dbContext.Set<TEntity>().ToListAsync();
    }
    public async Task<IQueryable<TEntity>> GetQueryable()
    {
        return dbContext.Set<TEntity>().AsQueryable();
    }
    public async Task<IQueryable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate)
    {
        return dbContext.Set<TEntity>().Where(predicate);
    }

    public async Task<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = dbContext.Set<TEntity>();
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }
        return await query.FirstOrDefaultAsync();
    }
    // parametreli include methodu tek varlýk getirir.
    public async Task<TEntity> GetWithWhereAndInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = dbContext.Set<TEntity>();
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }
        return await query.Where(predicate).FirstOrDefaultAsync();
    }
    // parametreli include methodu iliþkili tüm varlýklarý getirir.
    public async Task<List<TEntity>> GetAllWithWhereAndInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = dbContext.Set<TEntity>();
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }
        return await query.Where(predicate).ToListAsync();
    }
    // dinamik sorgu methodu.
    public async Task<List<TEntity>> GetWithDynamicQuery(string propertyName, string comparison, string value, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        // Query helper ile  dinamik sorgu oluþturuyoruz.
        var predicate = QueryHelper.BuildPredicate<TEntity>(propertyName, comparison, value);

        // where koþulumuza uygun olanlarý çekiyoruz.
        IQueryable<TEntity> query = dbContext.Set<TEntity>().Where(predicate);

        // Ýliþkili tablolarý sorguya dahil ediyoruz.
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return await query.ToListAsync();
    }
    //Dapper
    public async Task<List<CustomerReportDto>> GetCustomerReportAsync(string sqlQuery, object parameters = null)
    {
        using var connection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB; Database=padb;TrustServerCertificate=True;Integrated Security=true;");
        await connection.OpenAsync();

        var customerDict = new Dictionary<long, CustomerReportDto>();

        await connection.QueryAsync<CustomerReportDto, CustomerDetailDto, CustomerAddressDto, CustomerPhoneDto, CustomerReportDto>(
            sqlQuery,
            (customer, detail, address, phone) =>
            {
                if (!customerDict.TryGetValue(customer.Id, out var customerEntry))
                {
                    customerEntry = customer;
                    customerEntry.CustomerDetail = detail;
                    customerEntry.CustomerAddresses = new List<CustomerAddressDto>();
                    customerEntry.CustomerPhones = new List<CustomerPhoneDto>();
                    customerDict.Add(customer.Id, customerEntry);
                }
                // Address ve Telefon bilgisi liste halinde olacaðý için veri tekrarýný önlemek adýna eklendi.
                if (address != null && !customerEntry.CustomerAddresses.Any(a => a.AddressLine == address.AddressLine && a.City == address.City))
                {
                    customerEntry.CustomerAddresses.Add(address);
                }

                if (phone != null && !customerEntry.CustomerPhones.Any(p => p.Phone == phone.Phone))
                {
                    customerEntry.CustomerPhones.Add(phone);
                }

                return customerEntry;
            },
            parameters
        );

        return customerDict.Values.ToList();

    }

    public async Task<List<TResult>> GetReportAsync<TResult>(string sqlQuery, object parameters = null)
    {
        using var connection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB; Database=padb;TrustServerCertificate=True;Integrated Security=true;");
        await connection.OpenAsync();

        var result = await connection.QueryAsync<TResult>(sqlQuery, parameters);
        return result.ToList();
    }
}