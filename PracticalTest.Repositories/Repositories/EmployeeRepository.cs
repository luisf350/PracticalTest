using PracticalTest.Entities.Context;
using PracticalTest.Entities.Entities;
using PracticalTest.Repositories.Infrastructure;

namespace PracticalTest.Repositories.Repositories;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(AppDbContext context) : base(context)
    {
    }
}