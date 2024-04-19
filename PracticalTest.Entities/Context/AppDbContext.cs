using PracticalTest.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace PracticalTest.Entities.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
}
