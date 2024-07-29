using KrasOctTest.TreeComponents;

namespace KrasOctTest.Data;

public interface ITreeNodeRepository
{
    public Task CreateNode(string name, Node parentNode, NodeType type,
        bool editable = true);

    public Task UpdateNodeFields(int nodeId, string firstname, string lastname,
        string patronymic, DateTime? acceptedDate = null);
}