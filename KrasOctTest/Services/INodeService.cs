using KrasOctTest.TreeComponents;

namespace KrasOctTest.Services;

public interface INodeService
{
    public void AddChildToNode(Node parent, Node child);
}