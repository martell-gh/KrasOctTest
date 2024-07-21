using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KrasOctTest.TreeComponents;
using Microsoft.EntityFrameworkCore;

namespace KrasOctTest.Data;

public class TreeNodeData
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; }
    public NodeType NodeType { get; set; }
    public int? ParentNodeId { get; set; }
    public bool Editable { get; set; }
    
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Patronymic { get; set; }

    public DateTime? AcceptedDate { get; set; }

    [ForeignKey("ParentNodeId")]
    public virtual TreeNodeData ParentNode { get; set; }

    public static async Task EnsureDefaultNodeExists(TreeDbContext dbContext)
    {
        try
        {
            var defaultNodeId = 1;

            var defaultNode = await dbContext.TreeNodes.FirstOrDefaultAsync(node => node.Id == defaultNodeId);

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

                dbContext.TreeNodes.Add(defaultNode);
                await dbContext.SaveChangesAsync();
                Console.WriteLine("Создан новый узел с Id = " + defaultNode.Id);
            }
            else
            {
                Console.WriteLine("Узел с Id = 1 уже существует");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при создании узла: " + ex.Message);
            if (ex.InnerException != null)
            {
                Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
            }
            throw;
        }
    }

    public static async Task CreateNode(TreeDbContext dbContext, string name, Node parentNode, NodeType type, bool editable = true)
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
            
            dbContext.TreeNodes.Add(defaultNode);
            await dbContext.SaveChangesAsync();
            
            parentNode.AddChild(new Node(type, defaultNode.Id, name, editable));
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при создании узла: " + ex.Message);
            if (ex.InnerException != null)
            {
                Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
            }
            throw;
        }
    }

    public static async Task UpdateNodeFields(TreeDbContext dbContext, int nodeId, string firstname, string lastname, string patronymic, DateTime? acceptedDate = null)
    {
        try
        {
            var node = await dbContext.TreeNodes.FindAsync(nodeId);

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

            await dbContext.SaveChangesAsync();
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
