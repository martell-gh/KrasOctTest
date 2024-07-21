

namespace KrasOctTest.TreeComponents;

public class Node : TreeNode
{
    public int NodeId { get; }
    public NodeType NodeType { get; } = NodeType.EMPLOYEE;
    public bool Editable { get; } = true;
    public Node(NodeType nodeType, int id, string name, bool canEdit = true) : base(name) 
    {
        NodeType = nodeType;
        Editable = canEdit;
        NodeId = id;
    }

    public void AddChild(Node node)
    {
        if (this.NodeType == NodeType.EMPLOYEE)
        {
            throw new ArgumentException("Невозможно добавить дочерний узел сотруднику!");
        }
    
        base.Nodes.Add(node);
    }
}

public enum NodeType
{
    EMPLOYEE, DEPARTMENT
}