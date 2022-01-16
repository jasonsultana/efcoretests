using System;
using EFSamples.Data;
using EFSamples.Services;
using EFSamples.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EFSamples.Integration.Tests;

public class Tests
{
    private CustomerService _sut;
    private EFSamplesContext _dbContext;
    
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
            await _sut.AddCustomerAsync(customer);
        });
    }

    [TearDown]
    public void TearDown()
    {
        // Close the connection after the test has run
        _dbContext.Dispose();
    }
}