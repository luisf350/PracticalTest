using PracticalTest.Domain.Contract;
using PracticalTest.Entities.Entities;
using PracticalTest.Repositories.Repositories;

namespace PracticalTest.Domain.Impementation;

public class EmployeeDomain : DomainBase<Employee>, IEmployeeDomain
{
    public EmployeeDomain(IEmployeeRepository repository) : base(repository)
    {
    }
}
