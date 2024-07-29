using KrasOctTest.Data;
using KrasOctTest.TreeComponents;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using KrasOctTest.Data.Employees;
using KrasOctTest.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KrasOctTest
{
    public partial class MainForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        
        public Node CurrentNode;
        private readonly IDbContextFactory _dbContextFactory;
        private readonly ITreeNodeRepository _treeNodeRepository;
        private readonly INodeService _nodeService;
        
        private ApplicationDbContext _dbContext;

        public MainForm(IDbContextFactory dbContextFactory, ITreeNodeRepository treeNodeRepository, INodeService nodeService, IServiceProvider serviceProvider)
        {
            _dbContextFactory = dbContextFactory;
            _serviceProvider = serviceProvider;
            _treeNodeRepository = treeNodeRepository;
            _nodeService = nodeService;
            
            _dbContext = _dbContextFactory.CreateDbContext();

            InitializeDatabase();
            
            InitializeComponent();
            LoadTreeViewFromDatabaseAsync();
            
        }

        private async Task<bool> InitializeDatabase()
        {
            var initialized = await _dbContext.InitializeDatabase();
            return initialized;
        }

        public async Task LoadTreeViewFromDatabaseAsync()
        {
            treeViewDepartments.Nodes.Clear();

            try
            {
                var rootNodeData = await _dbContext.TreeNodes.FirstOrDefaultAsync(node => node.ParentNodeId == null);
                if (rootNodeData != null)
                {
                    var rootNode = ConvertToNode(rootNodeData);
                    treeViewDepartments.Nodes.Add(rootNode);

                    await AddChildNodesAsync(rootNode, rootNodeData.Id, _dbContext);
                }
                treeViewDepartments.ExpandAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex);
            }
        }

        private async Task AddChildNodesAsync(Node parentNode, int parentId, ApplicationDbContext dbContext)
        {
            var childNodesData = await dbContext.TreeNodes
                .Where(node => node.ParentNodeId == parentId)
                .ToListAsync();

            foreach (var childNodeData in childNodesData)
            {
                var childNode = ConvertToNode(childNodeData);
                _nodeService.AddChildToNode(parentNode, childNode);
                
                await AddChildNodesAsync(childNode, childNodeData.Id, dbContext);
            }
        }

        private Node ConvertToNode(TreeNodeData nodeData)
        {
            var nodeType = nodeData.NodeType == NodeType.EMPLOYEE ? NodeType.EMPLOYEE : NodeType.DEPARTMENT;
            return new Node(nodeType, nodeData.Id, nodeData.Name, nodeData.Editable);
        }

        private async void TreeViewDepartments_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null)
            {
                return;
            }

            var selectedNode = (Node)e.Node;
            CurrentNode = selectedNode;

            if (!selectedNode.Editable)
            {
                panelEmployee.Visible = false;
                this.button1.Enabled = true;
                this.button2.Enabled = false;
                this.button3.Enabled = false;
                return;
            }

            switch (selectedNode.NodeType)
            {
                case NodeType.EMPLOYEE:
                    panelEmployee.Visible = true;
                    this.button1.Enabled = false;
                    this.button2.Enabled = true;
                    this.button3.Enabled = true;
                    break;
                case NodeType.DEPARTMENT:
                    panelEmployee.Visible = false;
                    this.button1.Enabled = true;
                    this.button2.Enabled = true;
                    this.button3.Enabled = true;
                    break;
            }

            await UpdateFormFieldsAsync(CurrentNode);
        }

        private async Task UpdateFormFieldsAsync(Node selectedNode)
        {
            if (selectedNode == null)
            {
                return;
            }

            var node = await _dbContext.TreeNodes.FindAsync(selectedNode.NodeId);

            if (node != null)
            {
                textBoxFirstName.Text = node.Firstname ?? string.Empty;
                textBoxLastName.Text = node.Lastname ?? string.Empty;
                textBoxPatronymic.Text = node.Patronymic ?? string.Empty;
                dateTimePickerHireDate.Value = node.AcceptedDate ?? DateTime.Now;
            }
            else
            {
                textBoxFirstName.Clear();
                textBoxLastName.Clear();
                textBoxPatronymic.Clear();
                dateTimePickerHireDate.Value = DateTime.Now;
            }
        }

        public async Task UpdateInfo()
        {
            if (CurrentNode == null)
            {
                return;
            }

            try
            {
                await _treeNodeRepository.UpdateNodeFields(
                    CurrentNode.NodeId,
                    textBoxFirstName.Text,
                    textBoxLastName.Text,
                    textBoxPatronymic.Text,
                    dateTimePickerHireDate.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        
        private async void AnyFieldChanged(object sender, EventArgs e)
        {
            await UpdateInfo();
        }

        private async void ButtonSelect_Click(object sender, EventArgs e)
        {
            var employeesForm = _serviceProvider.GetRequiredService<SearchEmployee>();

            employeesForm.ShowDialog();
            if (employeesForm.DialogResult == DialogResult.OK)
            {
                await LoadTreeViewFromDatabaseAsync();
            }
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxPatronymic.Clear();
            dateTimePickerHireDate.Value = DateTime.Now;
            UpdateInfo();
        }

        private async void ButtonAddNode_Click(object sender, EventArgs e)
        {
            if (CurrentNode == null) return;
            NodeCreateForm newNodeForm = _serviceProvider.GetRequiredService<NodeCreateForm>();
            newNodeForm.ShowDialog();
            if (newNodeForm.DialogResult == DialogResult.OK)
            {
                await LoadTreeViewFromDatabaseAsync();
            }
        }

        private async void ButtonRemove_Click(object sender, EventArgs e)
        {
            await _dbContext.CascadeDeleteAsync(CurrentNode.NodeId);
            await LoadTreeViewFromDatabaseAsync();
        }
        
        private async void ButtonEdit_Click(object sender, EventArgs e)
        {
            RenameForm nameForm = new RenameForm(this, _dbContext);
            nameForm.ShowDialog();
        }
        
        private async Task UpdateHireDateAsync()
        {
            if (treeViewDepartments.SelectedNode == null)
            {
                MessageBox.Show("Пожалуйста, выберите узел для обновления.");
                return;
            }

            int nodeId; 
            if (!int.TryParse(treeViewDepartments.SelectedNode.Tag.ToString(), out nodeId))
            {
                MessageBox.Show("Не удалось получить идентификатор узла.");
                return;
            }

            string firstname = textBoxFirstName.Text;
            string lastname = textBoxLastName.Text;
            string patronymic = textBoxPatronymic.Text;
            DateTime hireDate = dateTimePickerHireDate.Value;

            try
            {
                await _treeNodeRepository.UpdateNodeFields(nodeId, firstname, lastname, patronymic, hireDate);
                MessageBox.Show("Дата принятия успешно обновлена.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении даты принятия: " + ex);
            }
        }

        private void DateTimePickerHireDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateHireDateAsync();
        }
    }
}
