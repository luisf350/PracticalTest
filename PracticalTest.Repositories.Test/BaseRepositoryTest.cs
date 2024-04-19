using PracticalTest.Entities.Context;
using PracticalTest.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PracticalTest.Repositories.Test;

public class BaseRepositoryTest
{
    protected AppDbContext Context;
    protected EmployeeRepository EmployeeRepository;


    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("Test")
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .Options;

        Context = new AppDbContext(options);

        EmployeeRepository = new EmployeeRepository(Context);
    }

    [TearDown]
    public void TearDown()
    {
        foreach (var item in EmployeeRepository.GetAll().Result)
        {
            _ = EmployeeRepository.Delete(item);
        }

        EmployeeRepository.Dispose();
        Context.Dispose();
    }
}
