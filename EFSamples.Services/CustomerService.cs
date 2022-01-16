using EFSamples.Data;
using EFSamples.Data.Entities;
using EFSamples.Services.DTOs;
using EFSamples.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace EFSamples.Services;

public class CustomerService
{
    private readonly IEFSamplesContext _dbContext;

    public CustomerService(IEFSamplesContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Customer> AddCustomerAsync(AddCustomerDto dto)
    {
        var customer = new Customer()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email
        };
        
        try
        {
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();

            return customer;
        }
        catch (DbUpdateException ex) when (ex.InnerException is MySqlException { ErrorCode: MySqlErrorCode.DuplicateKeyEntry })
        {
            Console.WriteLine(ex);

            // We need to detach the entry so Entity Framework won't insert the conflicting record after the old one is removed and we call SaveChangesAsync
            _dbContext.Entry(customer).State = EntityState.Detached;
            
            throw new DuplicateRecordException(nameof(Customer), nameof(Customer.Email), dto.Email);
        }
    }
}