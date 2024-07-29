using KrasOctTest.Data.Employees;
using KrasOctTest.TreeComponents;
using Microsoft.EntityFrameworkCore;

namespace KrasOctTest.Data;

public class ApplicationDbContext : DbContext
{
    
    public DbSet<TreeNodeData> TreeNodes { get; set; }
    public DbSet<EmployeeData> Employees { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER01;Database=master;Trusted_Connection=True;Encrypt=False;MultipleActiveResultSets=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TreeNodeData>(entity =>
        {
            entity.ToTable("TreeNodes");
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Firstname).HasMaxLength(50);
            entity.Property(e => e.Lastname).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);

            entity.HasOne<TreeNodeData>(e => e.ParentNode)
                .WithMany()
                .HasForeignKey(e => e.ParentNodeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        });
        
        modelBuilder.Entity<EmployeeData>(entity =>
        {
            entity.ToTable("Employees");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Firstname).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Lastname).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
        });
    }
    public async Task CascadeDeleteAsync(int nodeId)
    {
        var node = await TreeNodes.FindAsync(nodeId);
        if (node != null)
        {
            var allNodes = await TreeNodes.ToListAsync();
            var nodesToDelete = GetDescendants(nodeId, allNodes).ToList();

            nodesToDelete.Add(node);

            TreeNodes.RemoveRange(nodesToDelete);
            await SaveChangesAsync();
        }
    }

    private IEnumerable<TreeNodeData> GetDescendants(int nodeId, List<TreeNodeData> allNodes)
    {
        var nodesToDelete = new List<TreeNodeData>();
        var directChildren = allNodes.Where(node => node.ParentNodeId == nodeId).ToList();

        foreach (var child in directChildren)
        {
            nodesToDelete.Add(child);
            nodesToDelete.AddRange(GetDescendants(child.Id, allNodes));
        }

        return nodesToDelete;
    }
    
    public async Task<bool> TableExistsAsync(string tableName)
    {
        var sql = @"
            SELECT COUNT(*)
            FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_NAME = @tableName";

        var connection = this.Database.GetDbConnection();
        await using (var command = connection.CreateCommand())
        {
            command.CommandText = sql;
            command.CommandType = System.Data.CommandType.Text;

            var parameter = command.CreateParameter();
            parameter.ParameterName = "@tableName";
            parameter.Value = tableName;
            command.Parameters.Add(parameter);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            var result = (int)await command.ExecuteScalarAsync();

            return result > 0;
        }
    }
    
    public async Task<bool> InitializeDatabase()
    {
        try
        {
            bool treeTableIsCreated = await TableExistsAsync("TreeNodes");
            bool employeesTableIsCreated = await TableExistsAsync("Employees");

            if (!treeTableIsCreated)
            {
                Console.WriteLine("Таблицы TreeNodes не существует");
                return false;
            }
            if (!employeesTableIsCreated)
            {
                Console.WriteLine("Таблицы Employees не существует");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при инициализации базы данных: " + ex);
            throw;
            return false;
        }
    }

}