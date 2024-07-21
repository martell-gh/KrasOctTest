using System;
using System.Windows.Forms;
using KrasOctTest.Data.Employees;

namespace KrasOctTest
{
    public partial class InputForm : Form
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Patronymic { get; private set; }

        private EmployeeDbContext _dbContext;
        private DataGridView _dataGridView;
        
        public InputForm(EmployeeDbContext dbContext, DataGridView dataGridView)
        {
            _dbContext = dbContext;
            _dataGridView = dataGridView;
            
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            _dbContext.CreateEmployee(_dataGridView, textBoxFirstName.Text, textBoxLastName.Text, textBoxPatronymic.Text);
            
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }    
}