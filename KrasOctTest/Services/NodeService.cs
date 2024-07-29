using KrasOctTest.TreeComponents;

namespace KrasOctTest.Services;

public class NodeService : INodeService
{
    public void AddChildToNode(Node parent, Node child)
    {
        if (parent.NodeType == NodeType.EMPLOYEE)
        {
            throw new ArgumentException("Невозможно добавить дочерний узел сотруднику!");
        }
    
        parent.Nodes.Add(child);
    }
}