using KrasOctTest.Data;
using KrasOctTest.TreeComponents;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using KrasOctTest.Data.Employees;
using KrasOctTest.Services;
using Microsoft.EntityFrameworkCore;

namespace KrasOctTest
{
    public partial class MainForm : Form
    {
        public Node currentNode;
        private readonly ITreeDbContextFactory _treeDbContextFactory;
        private readonly IEmployeeDbContextFactory _employeeDbContextFactory;

        private EmployeeDbContext _employeeDbContext;
        private TreeDbContext _treeDbContext;

        public MainForm(ITreeDbContextFactory treeDbContextFactory, IEmployeeDbContextFactory employeeDbContextFactory)
        {
            _treeDbContextFactory = treeDbContextFactory;
            _employeeDbContextFactory = employeeDbContextFactory;
            
            _treeDbContext = _treeDbContextFactory.CreateDbContext();
            _employeeDbContext = _employeeDbContextFactory.CreateDbContext();

            InitializeComponent();
            InitializeDatabase();
            LoadTreeViewFromDatabaseAsync();
            
        }

        private async void InitializeDatabase()
        {
            await _treeDbContext.InitializeDatabase();
            await _employeeDbContext.InitializeDatabase();
        }

        public async Task LoadTreeViewFromDatabaseAsync()
        {
            using (var dbContext = _treeDbContextFactory.CreateDbContext())
            {
                treeViewDepartments.Nodes.Clear();

                try
                {
                    var rootNodeData = await dbContext.TreeNodes.FirstOrDefaultAsync(node => node.ParentNodeId == null);
                    if (rootNodeData != null)
                    {
                        var rootNode = ConvertToNode(rootNodeData);
                        treeViewDepartments.Nodes.Add(rootNode);

                        await AddChildNodesAsync(rootNode, rootNodeData.Id, dbContext);
                    }
                    treeViewDepartments.ExpandAll();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
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
            currentNode = selectedNode;

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

            await UpdateFormFieldsAsync(currentNode);
        }

        private async Task UpdateFormFieldsAsync(Node selectedNode)
        {
            if (selectedNode == null)
            {
                return;
            }

            var node = await _treeDbContext.TreeNodes.FindAsync(selectedNode.NodeId);

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
            if (currentNode == null)
            {
                return;
            }

            try
            {
                await TreeNodeData.UpdateNodeFields(
                    _treeDbContext,
                    currentNode.NodeId,
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

        private void ButtonSelect_Click(object sender, EventArgs e)
        {
            SearchEmployee employees = new SearchEmployee(this, _employeeDbContext, _employeeDbContextFactory);
            employees.Show();
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxPatronymic.Clear();
            dateTimePickerHireDate.Value = DateTime.Now;
            UpdateInfo();
        }

        private void ButtonAddNode_Click(object sender, EventArgs e)
        {
            if (currentNode == null) return;
            NodeCreateForm newNodeForm = new NodeCreateForm(_treeDbContext, currentNode);
            newNodeForm.ShowDialog();

        }

        private async void ButtonRemove_Click(object sender, EventArgs e)
        {
            await _treeDbContext.CascadeDeleteAsync(currentNode.NodeId);
            await LoadTreeViewFromDatabaseAsync();
        }
        
        private async void ButtonEdit_Click(object sender, EventArgs e)
        {
            RenameForm nameForm = new RenameForm(this, _treeDbContext);
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
                await TreeNodeData.UpdateNodeFields(_treeDbContext, nodeId, firstname, lastname, patronymic, hireDate);
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
