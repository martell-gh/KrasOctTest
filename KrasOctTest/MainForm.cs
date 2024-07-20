using KrasOctTest.Data;
using KrasOctTest.TreeComponents;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using KrasOctTest.Services;
using Microsoft.EntityFrameworkCore;

namespace KrasOctTest
{
    public partial class MainForm : Form
    {
        public Node currentNode;
        private readonly IDbContextFactory _dbContextFactory;
        private TreeDbContext _dbContext;

        public MainForm(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _dbContext = _dbContextFactory.CreateDbContext();

            InitializeComponent();
            InitializeDatabase();
            LoadTreeViewFromDatabaseAsync();
        }

        private void InitializeDatabase()
        {
            _dbContext.InitializeDatabase();
        }

        private async Task LoadTreeViewFromDatabaseAsync()
        {
            // Создайте новый экземпляр DbContext для этого вызова
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                treeViewDepartments.Nodes.Clear(); // Очищаем текущие узлы
                Console.WriteLine("test");

                try
                {
                    var rootNodeData = await dbContext.TreeNodes.FirstOrDefaultAsync(node => node.ParentNodeId == null);
                    if (rootNodeData != null)
                    {
                        var rootNode = ConvertToNode(rootNodeData);
                        treeViewDepartments.Nodes.Add(rootNode);

                        await AddChildNodesAsync(rootNode, rootNodeData.Id, dbContext);
                    }

                    // Разворачиваем все узлы
                    treeViewDepartments.ExpandAll();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception occurred while loading tree from database: " + ex.Message);
                }
            }
        }

        private async Task AddChildNodesAsync(Node parentNode, int parentId, TreeDbContext dbContext)
        {
            var childNodesData = await dbContext.TreeNodes
                .Where(node => node.ParentNodeId == parentId)
                .ToListAsync();

            foreach (var childNodeData in childNodesData)
            {
                var childNode = ConvertToNode(childNodeData);
                parentNode.AddChild(childNode);

                // Рекурсивно добавляем дочерние узлы
                await AddChildNodesAsync(childNode, childNodeData.Id, dbContext);
            }
        }

        private Node ConvertToNode(TreeNodeData nodeData)
        {
            var nodeType = nodeData.NodeType == NodeType.EMPLOYEE ? NodeType.EMPLOYEE : NodeType.DEPARTMENT;
            return new Node(nodeType, nodeData.Id, nodeData.Name, nodeData.Editable);
        }

        private void TreeViewDepartments_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Node selectedNode = (Node)e.Node;
            currentNode = selectedNode;

            if (!selectedNode.Editable)
            {
                panelEmployee.Visible = false;
                panelDepartment.Visible = false;
                buttonAddNode.Enabled = true;
                return;
            }

            switch (selectedNode.NodeType)
            {
                case NodeType.EMPLOYEE:
                    panelEmployee.Visible = true;
                    panelDepartment.Visible = false;
                    buttonAddNode.Enabled = false;
                    break;
                case NodeType.DEPARTMENT:
                    panelEmployee.Visible = false;
                    panelDepartment.Visible = true;
                    buttonAddNode.Enabled = true;
                    break;
            }
            
        }

        private void ButtonSelect_Click(object sender, EventArgs e)
        {
            // Логика для обработки нажатия на кнопку "Выбрать"
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            
        }

        private void ButtonAddNode_Click(object sender, EventArgs e)
        {
            NodeCreateForm newNodeForm = new NodeCreateForm(_dbContext, currentNode);
            newNodeForm.ShowDialog();

        }

        private async void ButtonHideDetails_Click(object sender, EventArgs e)
        {
            await _dbContext.CascadeDeleteAsync(currentNode.NodeId);
            await LoadTreeViewFromDatabaseAsync();
        }
    }
}
