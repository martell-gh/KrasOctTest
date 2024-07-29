using System;
using System.Windows.Forms;
using KrasOctTest.Data;
using KrasOctTest.Data.Employees;
using KrasOctTest.Services;
using KrasOctTest.TreeComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KrasOctTest
{
    public partial class SearchEmployee : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEmployeeRepository _employeeRepository;
        
        private DataGridViewRow _selectedRow;
        
        private string _oldLastName;
        private string _oldFirstName;
        private string _oldPatronymic;
        private int _currentRowIndex;

        private MainForm _mainForm;

        public SearchEmployee(MainForm form, IServiceProvider serviceProvider, IEmployeeRepository employeeRepository)
        {
            _mainForm = form;
            _serviceProvider = serviceProvider;
            _employeeRepository = employeeRepository;
            
            InitializeComponent();
            LoadEmployeesToDataGridViewAsync();
            this.MinimumSize = new System.Drawing.Size(750, 400);
            
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
            dataGridView1.CellBeginEdit += DataGridView1_CellBeginEdit;
            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;

        }
        private void ShowInputForm()
        {
            var inputForm = _serviceProvider.GetRequiredService<InputForm>();

            inputForm.ShowDialog();
        }
        
        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];
                _currentRowIndex = e.RowIndex;
                _oldLastName = row.Cells["ColumnLastName"].Value.ToString();
                _oldFirstName = row.Cells["ColumnFirstName"].Value.ToString();
                _oldPatronymic = row.Cells["ColumnPatronymic"].Value.ToString();
            }
        }

        private async void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];

                var newLastName = row.Cells["ColumnLastName"].Value.ToString();
                var newFirstName = row.Cells["ColumnFirstName"].Value.ToString();
                var newPatronymic = row.Cells["ColumnPatronymic"].Value.ToString();

                await _employeeRepository.UpdateEmployeeInDatabase(_oldLastName, _oldFirstName, _oldPatronymic, newLastName, newFirstName, newPatronymic);
            }
        }
        
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string firstName = textBoxFirstName.Text.ToLower();
            string lastName = textBoxLastName.Text.ToLower();
            string patronymic = textBoxPatronymic.Text.ToLower();
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Visible = true;
                    if (!string.IsNullOrWhiteSpace(firstName))
                    {
                        if (!row.Cells["ColumnFirstName"].Value.ToString().ToLower().Contains(firstName))
                            row.Visible = false;
                    }

                    if (!string.IsNullOrWhiteSpace(lastName) &&
                        !row.Cells["ColumnLastName"].Value.ToString().ToLower().Contains(lastName))
                    {
                        row.Visible = false;
                    }

                    if (!string.IsNullOrWhiteSpace(patronymic) && !row.Cells["ColumnPatronymic"].Value.ToString()
                            .ToLower().Contains(patronymic))
                    {
                        row.Visible = false;
                    }
                }
            }
            catch
            {
                
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    _selectedRow = dataGridView1.Rows[e.RowIndex];
                    this.toolStripButton2.Enabled = true;
                    this.toolStripButton3.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ShowInputForm();
        }

        private async void toolStripButton2_Click(object sender, EventArgs e)
        {
            _mainForm.textBoxFirstName.Text = _selectedRow.Cells["ColumnLastName"].Value.ToString();
            _mainForm.textBoxLastName.Text = _selectedRow.Cells["ColumnFirstName"].Value.ToString();
            _mainForm.textBoxPatronymic.Text = _selectedRow.Cells["ColumnPatronymic"].Value.ToString();
            await _mainForm.UpdateInfo();

            DialogResult = DialogResult.OK;

            this.Close();
        }

        private async void toolStripButton3_Click(object sender, EventArgs e)
        {
            string lastName = _selectedRow.Cells["ColumnLastName"].Value.ToString();
            string firstName = _selectedRow.Cells["ColumnFirstName"].Value.ToString();
            string patronymic = _selectedRow.Cells["ColumnPatronymic"].Value.ToString();

            try
            {
                await _employeeRepository.DeleteEmployee(lastName, firstName, patronymic);
                LoadEmployeesToDataGridViewAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex}");
            }
        }
        
        
        public async Task LoadEmployeesToDataGridViewAsync()
        {
            try
            {
                var employees = await _employeeRepository.GetEmployeesAsync();

                dataGridView1.Rows.Clear();

                foreach (var employee in employees)
                {
                    dataGridView1.Rows.Add(employee.Lastname, employee.Firstname, employee.Patronymic);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Ошибка загрузки сотрудников: {ex}");
                Console.WriteLine(ex);
            }
        }

    }
}