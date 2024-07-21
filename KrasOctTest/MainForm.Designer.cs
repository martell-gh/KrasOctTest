using KrasOctTest.TreeComponents;

namespace KrasOctTest
{
    partial class MainForm
    {
        private System.Windows.Forms.TreeView treeViewDepartments;
        private System.Windows.Forms.Label labelFirstName;
        public System.Windows.Forms.TextBox textBoxLastName;
        private System.Windows.Forms.Label labelLastName;
        public System.Windows.Forms.TextBox textBoxFirstName;
        private System.Windows.Forms.Label labelPatronymic;
        public System.Windows.Forms.TextBox textBoxPatronymic;
        private System.Windows.Forms.Label labelHireDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerHireDate;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Panel panelEmployee;
        private System.Windows.Forms.TextBox textBoxAlternative;
        private System.Windows.Forms.Button buttonAlternative;
        private ToolStripButton button1;
        private ToolStripButton button2;
        private ToolStripButton button3;
        
        private void InitializeComponent()
        {
            this.treeViewDepartments = new System.Windows.Forms.TreeView();
            this.labelFirstName = new System.Windows.Forms.Label();
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.labelLastName = new System.Windows.Forms.Label();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.labelPatronymic = new System.Windows.Forms.Label();
            this.textBoxPatronymic = new System.Windows.Forms.TextBox();
            this.labelHireDate = new System.Windows.Forms.Label();
            this.dateTimePickerHireDate = new System.Windows.Forms.DateTimePicker();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.panelEmployee = new System.Windows.Forms.Panel();
            this.textBoxAlternative = new System.Windows.Forms.TextBox();
            this.buttonAlternative = new System.Windows.Forms.Button();
            this.SuspendLayout();
            
            this.treeViewDepartments.Location = new System.Drawing.Point(12, 40);
            this.treeViewDepartments.Size = new System.Drawing.Size(250, 400);
            this.treeViewDepartments.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewDepartments_AfterSelect);
            this.treeViewDepartments.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            
            this.panelEmployee.Location = new System.Drawing.Point(270, 50);
            this.panelEmployee.Size = new System.Drawing.Size(320, 400);
            this.panelEmployee.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            this.panelEmployee.Controls.Add(this.labelFirstName);
            this.panelEmployee.Controls.Add(this.textBoxLastName);
            this.panelEmployee.Controls.Add(this.labelLastName);
            this.panelEmployee.Controls.Add(this.textBoxFirstName);
            this.panelEmployee.Controls.Add(this.labelPatronymic);
            this.panelEmployee.Controls.Add(this.textBoxPatronymic);
            this.panelEmployee.Controls.Add(this.labelHireDate);
            this.panelEmployee.Controls.Add(this.dateTimePickerHireDate);
            this.panelEmployee.Controls.Add(this.buttonSelect);
            this.panelEmployee.Controls.Add(this.buttonClear);
            
            this.textBoxAlternative.Location = new System.Drawing.Point(10, 10);
            this.textBoxAlternative.Size = new System.Drawing.Size(300, 20);
            this.textBoxAlternative.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            
            this.buttonAlternative.Location = new System.Drawing.Point(10, 40);
            this.buttonAlternative.Size = new System.Drawing.Size(75, 23);
            this.buttonAlternative.Text = "Кнопка";
            this.buttonAlternative.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
            
            this.labelFirstName.AutoSize = true;
            this.labelFirstName.Location = new System.Drawing.Point(0, 15);
            this.labelFirstName.Text = "Имя:";
            
            this.textBoxLastName.Location = new System.Drawing.Point(140, 12);
            this.textBoxLastName.Size = new System.Drawing.Size(180, 20);
            this.textBoxLastName.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            
            this.labelLastName.AutoSize = true;
            this.labelLastName.Location = new System.Drawing.Point(0, 45);
            this.labelLastName.Text = "Фамилия:";
            
            this.textBoxFirstName.Location = new System.Drawing.Point(140, 42);
            this.textBoxFirstName.Size = new System.Drawing.Size(180, 20);
            this.textBoxFirstName.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            
            this.labelPatronymic.AutoSize = true;
            this.labelPatronymic.Location = new System.Drawing.Point(0, 75);
            this.labelPatronymic.Text = "Отчество:";
            
            this.textBoxPatronymic.Location = new System.Drawing.Point(140, 72);
            this.textBoxPatronymic.Size = new System.Drawing.Size(180, 20);
            this.textBoxPatronymic.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            
            this.labelHireDate.AutoSize = true;
            this.labelHireDate.Location = new System.Drawing.Point(0, 105);
            this.labelHireDate.Text = "Дата принятия:";

            this.dateTimePickerHireDate.Location = new System.Drawing.Point(140, 102);
            this.dateTimePickerHireDate.Size = new System.Drawing.Size(90, 20);
            this.dateTimePickerHireDate.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            this.dateTimePickerHireDate.ValueChanged += new System.EventHandler(this.DateTimePickerHireDate_ValueChanged);

            this.buttonSelect.Location = new System.Drawing.Point(0, 140);
            this.buttonSelect.Size = new System.Drawing.Size(90, 30);
            this.buttonSelect.Text = "Выбрать";
            this.buttonSelect.Click += new System.EventHandler(this.ButtonSelect_Click);

            this.buttonClear.Location = new System.Drawing.Point(205, 140);
            this.buttonClear.Size = new System.Drawing.Size(90, 30);
            this.buttonClear.Text = "Очистить";
            this.buttonClear.Click += new System.EventHandler(this.ButtonClear_Click);
            this.buttonClear.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);

            this.Controls.Add(this.treeViewDepartments);
            this.Controls.Add(this.panelEmployee);

            ToolStrip toolStrip = new ToolStrip();
            toolStrip.Dock = DockStyle.Top;

            button1 = new ToolStripButton();
            button2 = new ToolStripButton();
            button3 = new ToolStripButton();
            button1.Image = Image.FromFile("Resources/plus.png");
            button1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            button1.Click += new System.EventHandler(this.ButtonAddNode_Click);

            button2.Image = Image.FromFile("Resources/edit.png");
            button2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            button2.Click += new System.EventHandler(this.ButtonEdit_Click);

            button3.Image = Image.FromFile("Resources/cross.png");
            button3.DisplayStyle = ToolStripItemDisplayStyle.Image;
            button3.Click += new System.EventHandler(this.ButtonRemove_Click);

            toolStrip.Items.Add(button1);
            toolStrip.Items.Add(button2);
            toolStrip.Items.Add(button3);
            
            this.button2.Enabled = false;
            this.button3.Enabled = false;
            this.panelEmployee.Visible = false;

            this.Controls.Add(toolStrip);

            this.textBoxFirstName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnyFieldChanged);
            this.textBoxLastName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnyFieldChanged);
            this.textBoxPatronymic.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnyFieldChanged);

            this.Text = "Структура предприятия";
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        
    }
}
