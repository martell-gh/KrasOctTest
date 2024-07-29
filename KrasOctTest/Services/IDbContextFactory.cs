using KrasOctTest.Data;

namespace KrasOctTest.Services;

public interface IDbContextFactory
{
    ApplicationDbContext CreateDbContext();
}