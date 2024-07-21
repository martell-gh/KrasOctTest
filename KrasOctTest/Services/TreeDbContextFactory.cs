using KrasOctTest.Data;
using Microsoft.Extensions.DependencyInjection;

namespace KrasOctTest.Services;

public interface ITreeDbContextFactory
{
    TreeDbContext CreateDbContext();
}

public class TreeDbContextFactory : ITreeDbContextFactory
{
    private readonly IServiceProvider _serviceProvider;

    public TreeDbContextFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TreeDbContext CreateDbContext()
    {
        return _serviceProvider.GetRequiredService<TreeDbContext>();
    }
}