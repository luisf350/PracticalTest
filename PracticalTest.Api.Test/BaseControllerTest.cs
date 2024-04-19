using AutoMapper;
using PracticalTest.Api.Controllers;
using PracticalTest.Api.Profiles;
using PracticalTest.Domain.Contract;
using PracticalTest.Domain.Impementation;
using PracticalTest.Repositories.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace PracticalTest.Api.Test;

public class BaseControllerTest<TController>
    where TController : BaseController
{
    protected IEmployeeDomain EmployeeDomain;
    protected IMapper Mapper;
    protected Mock<ILogger<TController>> LoggerController;

    protected Mock<IEmployeeRepository> EmployeeRepositoryMock = new Mock<IEmployeeRepository>();

    [SetUp]
    public void Setup()
    {
        // Logger
        LoggerController = new Mock<ILogger<TController>>();

        // Domains
        EmployeeDomain = new EmployeeDomain(EmployeeRepositoryMock.Object);

        // AutoMapper
        Mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();
    }
}
