using PracticalTest.Domain.Contract;
using PracticalTest.Domain.Impementation;
using PracticalTest.Repositories.Repositories;

namespace PracticalTest.Api.Extensions;

public static class DiExtensions
{
    public static void AddDiExtensions(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeDomain, EmployeeDomain>();

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    }
}
