using KrasOctTest.Data;

namespace KrasOctTest
{

    public partial class RenameForm : Form
    {
        private MainForm _mainForm;
        private ApplicationDbContext _dbContext;

        public RenameForm(MainForm mainForm, ApplicationDbContext dbContext)
        {
            _mainForm = mainForm;
            _dbContext = dbContext;
            InitializeComponent();
        }

        private async void ButtonOK_Click(object sender, EventArgs e)
        {
            Console.WriteLine(textBoxName.Text);
            _mainForm.CurrentNode.Name = textBoxName.Text;
             
            var node = await _dbContext.TreeNodes.FindAsync(_mainForm.CurrentNode.NodeId);

            if (node == null) {
                 Console.WriteLine($"Узел с Id = {_mainForm.CurrentNode.NodeId} не найден.");
                 return;
            }

            node.Name = textBoxName.Text;

            await _dbContext.SaveChangesAsync();
            await _mainForm.LoadTreeViewFromDatabaseAsync();
            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}