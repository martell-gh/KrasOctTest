using System;
using System.Windows.Forms;
using KrasOctTest.Data;
using KrasOctTest.Data.Employees;

namespace KrasOctTest
{
    public partial class InputForm : Form
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Patronymic { get; private set; }

        private IEmployeeRepository _employeeRepository;
        private SearchEmployee _searchEmployeeWindow;
        
        private ApplicationDbContext _dbContext;
        private DataGridView _dataGridView;
        
        public InputForm(IEmployeeRepository employeeRepository, SearchEmployee searchEmployeeWindow)
        {
            _employeeRepository = employeeRepository;
            _searchEmployeeWindow = searchEmployeeWindow;
            
            InitializeComponent();
        }

        private async void buttonOk_Click(object sender, EventArgs e)
        {
            await _employeeRepository.CreateEmployee(textBoxFirstName.Text, textBoxLastName.Text, textBoxPatronymic.Text);
            await _searchEmployeeWindow.LoadEmployeesToDataGridViewAsync();
            
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }    
}