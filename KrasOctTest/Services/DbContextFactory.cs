using KrasOctTest.Data;
using Microsoft.Extensions.DependencyInjection;

namespace KrasOctTest.Services;

public interface IDbContextFactory
{
    TreeDbContext CreateDbContext();
}

public class DbContextFactory : IDbContextFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DbContextFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TreeDbContext CreateDbContext()
    {
        return _serviceProvider.GetRequiredService<TreeDbContext>();
    }
}