using KrasOctTest.TreeComponents;
using Microsoft.EntityFrameworkCore;

namespace KrasOctTest.Data;

public class TreeDbContext : DbContext
{
    public DbSet<TreeNodeData> TreeNodes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Здесь указывается строка подключения к вашей базе данных
        optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER01;Database=master;Trusted_Connection=True;Encrypt=False;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Указываем конфигурацию для таблицы TreeNodes
        modelBuilder.Entity<TreeNodeData>(entity =>
        {
            entity.ToTable("TreeNodes"); // Указываем имя таблицы
            entity.HasKey(e => e.Id); // Задаем первичный ключ

            // Удаляем текущее определение внешнего ключа для ParentNodeId
            // и явно задаем, что ParentNodeId ссылается на Id таблицы TreeNodes
            entity.HasOne<TreeNodeData>(e => e.ParentNode)
                .WithMany()
                .HasForeignKey(e => e.ParentNodeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction); // Изменение или удаление не выполняется
        });
    }
    public async Task CascadeDeleteAsync(int nodeId)
    {
        var node = await TreeNodes.FindAsync(nodeId);
        if (node != null)
        {
            var allNodes = await TreeNodes.ToListAsync();
            var nodesToDelete = GetDescendants(nodeId, allNodes).ToList();

            // Добавляем сам узел в список узлов для удаления
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

    // Инициализация базы данных и создание узла по умолчанию
    public async Task InitializeDatabase()
    {
        try
        {
            // Проверяем, создана ли уже таблица TreeNodes
            bool isCreated = Database.EnsureCreated();

            if (isCreated)
            {
                Console.WriteLine("Таблица TreeNodes создана.");
            }
            else
            {
                Console.WriteLine("Таблица TreeNodes уже существует.");
            }

            // Запускаем метод для создания узла по умолчанию
            await TreeNodeData.EnsureDefaultNodeExists(this);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception occurred: " + ex.Message);
            throw; // Перебрасываем исключение для обработки выше по стеку вызовов
        }
    }

}