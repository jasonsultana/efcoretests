using System.Collections.Generic;
using System.Threading.Tasks;
using EFSamples.Data;
using EFSamples.Data.Entities;
using EFSamples.Services;
using EFSamples.Services.DTOs;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

namespace EFSamples.Unit.Tests;

public class Tests
{
    private CustomerService _sut;

    private List<Customer> _customers = new List<Customer>();
    
    [SetUp]
    public void Setup()
    {
        var mock = new Mock<IEFSamplesContext>();
        mock.Setup(m => m.Customers)
            .ReturnsDbSet(_customers); // This magic comes from Moq.EntityFrameworkCore

        _sut = new CustomerService(mock.Object);
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
}