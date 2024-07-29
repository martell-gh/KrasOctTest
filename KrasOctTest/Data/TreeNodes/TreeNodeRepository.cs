using KrasOctTest.TreeComponents;
using Microsoft.EntityFrameworkCore;

namespace KrasOctTest.Data;

public class TreeNodeRepository : ITreeNodeRepository
{
    private ApplicationDbContext _dbContext;

    public TreeNodeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        EnsureDefaultNodeExists();
    }

    private async Task EnsureDefaultNodeExists()
    {
        try
        {
            var defaultNodeId = 1;

            var defaultNode = await _dbContext.TreeNodes.FirstOrDefaultAsync(node => node.Id == defaultNodeId);

            if (defaultNode == null)
            {
                defaultNode = new TreeNodeData
                {
                    Name = "Заводоуправление",
                    NodeType = NodeType.DEPARTMENT,
                    ParentNodeId = null,
                    Editable = false,
                    Firstname = "",
                    Lastname = "",
                    Patronymic = "",
                    AcceptedDate = DateTime.UtcNow
                };
                _dbContext.TreeNodes.Add(defaultNode);
                await _dbContext.SaveChangesAsync();
                Console.WriteLine("Создан новый узел с Id = " + defaultNode.Id);
            }
            else
            {
                Console.WriteLine("Узел с Id = 1 уже существует");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при создании узла: " + ex);
            throw;
        }
    }
    
    public async Task CreateNode(string name, Node parentNode, NodeType type, bool editable = true)
    {
        try
        {
            var defaultNode = new TreeNodeData
            {
                Name = name,
                NodeType = type,
                ParentNodeId = parentNode.NodeId,
                Editable = editable,
                Firstname = "",
                Lastname = "",
                Patronymic = "",
                AcceptedDate = DateTime.UtcNow
            };
            
            _dbContext.TreeNodes.Add(defaultNode);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при создании узла: " + ex);
            throw;
        }
    }
    
    public async Task UpdateNodeFields(int nodeId, string firstname, string lastname, string patronymic, DateTime? acceptedDate = null)
    {
        try
        {
            var node = await _dbContext.TreeNodes.FindAsync(nodeId);

            if (node == null)
            {
                Console.WriteLine($"Узел с Id = {nodeId} не найден.");
                return;
            }
            
            node.Firstname = firstname;
            node.Lastname = lastname;
            node.Patronymic = patronymic;
            
            if (acceptedDate.HasValue)
            {
                node.AcceptedDate = acceptedDate.Value;
            }

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при обновлении узла: " + ex.Message);
            if (ex.InnerException != null)
            {
                Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
            }
            throw;
        }
    }

}