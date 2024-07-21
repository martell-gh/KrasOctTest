
using KrasOctTest.Data.Employees;
using Microsoft.Extensions.DependencyInjection;

namespace KrasOctTest.Services;

public interface IEmployeeDbContextFactory
{
    EmployeeDbContext CreateDbContext();
}

public class EmployeeDbContextFactory : IEmployeeDbContextFactory
{
    private readonly IServiceProvider _serviceProvider;

    public EmployeeDbContextFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public EmployeeDbContext CreateDbContext()
    {
        return _serviceProvider.GetRequiredService<EmployeeDbContext>();
    }
}