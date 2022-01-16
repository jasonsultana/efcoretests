using EFSamples.Data;
using EFSamples.Data.Entities;
using EFSamples.Services.DTOs;

namespace EFSamples.Services;

public class CustomerService
{
    private readonly IEFSamplesContext _dbContext;

    public CustomerService(IEFSamplesContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddCustomerAsync(AddCustomerDto dto)
    {
        try
        {
            var customer = new Customer()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}