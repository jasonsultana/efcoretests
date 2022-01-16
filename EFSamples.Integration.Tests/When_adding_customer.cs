using System;
using System.Threading.Tasks;
using EFSamples.Data;
using EFSamples.Data.Entities;
using EFSamples.Services;
using EFSamples.Services.DTOs;
using EFSamples.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EFSamples.Integration.Tests;

public class Tests
{
    private CustomerService _sut;
    private EFSamplesContext _dbContext;
    private Customer _customer;
    
    [SetUp]
    public void Setup()
    {
        _dbContext = new EFSamplesContext();
        _sut = new CustomerService(_dbContext);
    }

    [Test]
    public void Customer_can_be_added()
    {
        var customer = new AddCustomerDto("John", "Doe", "john.doe@gmail.com");
        Assert.DoesNotThrowAsync(async () =>
        {
            _customer = await _sut.AddCustomerAsync(customer);
        });
    }

    [Test]
    public void Duplicate_emails_are_not_permitted()
    {
        var firstCustomer = new AddCustomerDto("Jack", "Daniels", "jack.daniels@gmail.com");
        // First insertion should not throw
        Assert.DoesNotThrowAsync(async () =>
        {
            _customer = await _sut.AddCustomerAsync(firstCustomer);
        });

        var secondCustomer = new AddCustomerDto("Jack2", "Daniels2", "jack.daniels@gmail.com");
        // Second insertion should complain about the duplicate email
        Assert.ThrowsAsync<DuplicateRecordException>(async () =>
        {
            await _sut.AddCustomerAsync(secondCustomer);
        });
    }

    [TearDown]
    public async Task TearDown()
    {
        // Remove the row to clean up
        _dbContext.Customers.Remove(_customer);
        await _dbContext.SaveChangesAsync();
        
        // Close the connection after the test has run
        await _dbContext.DisposeAsync();
    }
}