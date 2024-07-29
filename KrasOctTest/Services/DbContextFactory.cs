using KrasOctTest.Data;
using Microsoft.Extensions.DependencyInjection;

namespace KrasOctTest.Services;

public class DbContextFactory : IDbContextFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DbContextFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ApplicationDbContext CreateDbContext()
    {
        return _serviceProvider.GetRequiredService<ApplicationDbContext>();
    }
}