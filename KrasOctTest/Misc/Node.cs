
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
}