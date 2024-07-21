using KrasOctTest.TreeComponents;
using Microsoft.EntityFrameworkCore;

namespace KrasOctTest.Data;

public class TreeDbContext : DbContext
{
    public DbSet<TreeNodeData> TreeNodes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER01;Database=master;Trusted_Connection=True;Encrypt=False;");
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

    public async Task InitializeDatabase()
    {
        try
        {
            bool isCreated = Database.EnsureCreated();

            if (isCreated)
            {
                Console.WriteLine("Таблица TreeNodes создана.");
            }
            else
            {
                Console.WriteLine("Таблица TreeNodes уже существует.");
            }
            
            await TreeNodeData.EnsureDefaultNodeExists(this);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception occurred: " + ex.Message);
            throw;
        }
    }

}