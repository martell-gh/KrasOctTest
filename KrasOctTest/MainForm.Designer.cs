using KrasOctTest.TreeComponents;

namespace KrasOctTest
{
    partial class MainForm
    {
        private System.Windows.Forms.TreeView treeViewDepartments;
        private System.Windows.Forms.Label labelLastName;
        private System.Windows.Forms.TextBox textBoxLastName;
        private System.Windows.Forms.Label labelFirstName;
        private System.Windows.Forms.TextBox textBoxFirstName;
        private System.Windows.Forms.Label labelMiddleName;
        private System.Windows.Forms.TextBox textBoxMiddleName;
        private System.Windows.Forms.Label labelHireDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerHireDate;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Panel panelEmployee;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panelDepartment;
        private System.Windows.Forms.TextBox textBoxAlternative;
        private System.Windows.Forms.Button buttonAlternative;
        private System.Windows.Forms.Button buttonAddNode;
        private System.Windows.Forms.Button buttonHideDetails;

        // Метод для инициализации компонентов формы
        private void InitializeComponent()
        {
            this.treeViewDepartments = new System.Windows.Forms.TreeView();
            this.labelLastName = new System.Windows.Forms.Label();
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.labelFirstName = new System.Windows.Forms.Label();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.labelMiddleName = new System.Windows.Forms.Label();
            this.textBoxMiddleName = new System.Windows.Forms.TextBox();
            this.labelHireDate = new System.Windows.Forms.Label();
            this.dateTimePickerHireDate = new System.Windows.Forms.DateTimePicker();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.panelEmployee = new System.Windows.Forms.Panel();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.panelDepartment = new System.Windows.Forms.Panel();
            this.textBoxAlternative = new System.Windows.Forms.TextBox();
            this.buttonAlternative = new System.Windows.Forms.Button();
            this.buttonAddNode = new System.Windows.Forms.Button();
            this.buttonHideDetails = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // TreeView для структуры предприятия
            this.treeViewDepartments.Location = new System.Drawing.Point(12, 50);
            this.treeViewDepartments.Size = new System.Drawing.Size(250, 400);
            this.treeViewDepartments.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewDepartments_AfterSelect);
            this.treeViewDepartments.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);

            // Panel для деталей сотрудника
            this.panelEmployee.Location = new System.Drawing.Point(270, 50);
            this.panelEmployee.Size = new System.Drawing.Size(320, 400);
            this.panelEmployee.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            this.panelEmployee.Controls.Add(this.labelLastName);
            this.panelEmployee.Controls.Add(this.textBoxLastName);
            this.panelEmployee.Controls.Add(this.labelFirstName);
            this.panelEmployee.Controls.Add(this.textBoxFirstName);
            this.panelEmployee.Controls.Add(this.labelMiddleName);
            this.panelEmployee.Controls.Add(this.textBoxMiddleName);
            this.panelEmployee.Controls.Add(this.labelHireDate);
            this.panelEmployee.Controls.Add(this.dateTimePickerHireDate);
            this.panelEmployee.Controls.Add(this.buttonSelect);
            this.panelEmployee.Controls.Add(this.buttonClear);

            // Panel для кнопок управления
            this.panelButtons.Location = new System.Drawing.Point(12, 12);
            this.panelButtons.Size = new System.Drawing.Size(578, 30);
            this.panelButtons.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            this.panelButtons.Controls.Add(this.buttonAddNode);
            this.panelButtons.Controls.Add(this.buttonHideDetails);

            // Panel для альтернативного содержимого
            this.panelDepartment.Location = new System.Drawing.Point(270, 50);
            this.panelDepartment.Size = new System.Drawing.Size(320, 400);
            this.panelDepartment.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            this.panelDepartment.Controls.Add(this.textBoxAlternative);
            this.panelDepartment.Controls.Add(this.buttonAlternative);
            this.panelDepartment.Visible = false;

            // TextBox для альтернативного содержимого
            this.textBoxAlternative.Location = new System.Drawing.Point(10, 10);
            this.textBoxAlternative.Size = new System.Drawing.Size(300, 20);
            this.textBoxAlternative.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);

            // Button для альтернативного содержимого
            this.buttonAlternative.Location = new System.Drawing.Point(10, 40);
            this.buttonAlternative.Size = new System.Drawing.Size(75, 23);
            this.buttonAlternative.Text = "Кнопка";
            this.buttonAlternative.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);

            // Label для фамилии
            this.labelLastName.AutoSize = true;
            this.labelLastName.Location = new System.Drawing.Point(0, 15);
            this.labelLastName.Text = "Фамилия:";

            // TextBox для ввода фамилии
            this.textBoxLastName.Location = new System.Drawing.Point(140, 12);
            this.textBoxLastName.Size = new System.Drawing.Size(180, 20);
            this.textBoxLastName.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);

            // Label для имени
            this.labelFirstName.AutoSize = true;
            this.labelFirstName.Location = new System.Drawing.Point(0, 45);
            this.labelFirstName.Text = "Имя:";

            // TextBox для ввода имени
            this.textBoxFirstName.Location = new System.Drawing.Point(140, 42);
            this.textBoxFirstName.Size = new System.Drawing.Size(180, 20);
            this.textBoxFirstName.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);

            // Label для отчества
            this.labelMiddleName.AutoSize = true;
            this.labelMiddleName.Location = new System.Drawing.Point(0, 75);
            this.labelMiddleName.Text = "Отчество:";

            // TextBox для ввода отчества
            this.textBoxMiddleName.Location = new System.Drawing.Point(140, 72);
            this.textBoxMiddleName.Size = new System.Drawing.Size(180, 20);
            this.textBoxMiddleName.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);

            // Label для даты принятия
            this.labelHireDate.AutoSize = true;
            this.labelHireDate.Location = new System.Drawing.Point(0, 105);
            this.labelHireDate.Text = "Дата принятия:";

            // DateTimePicker для выбора даты принятия
            this.dateTimePickerHireDate.Location = new System.Drawing.Point(140, 102);
            this.dateTimePickerHireDate.Size = new System.Drawing.Size(90, 20);
            this.dateTimePickerHireDate.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);

            // Кнопка "Выбрать"
            this.buttonSelect.Location = new System.Drawing.Point(0, 140);
            this.buttonSelect.Size = new System.Drawing.Size(90, 30);
            this.buttonSelect.Text = "Выбрать";
            this.buttonSelect.Click += new System.EventHandler(this.ButtonSelect_Click);

            // Кнопка "Очистить"
            this.buttonClear.Location = new System.Drawing.Point(205, 140);
            this.buttonClear.Size = new System.Drawing.Size(90, 30);
            this.buttonClear.Text = "Очистить";
            this.buttonClear.Click += new System.EventHandler(this.ButtonClear_Click);
            this.buttonClear.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);

            // Кнопка "Показать детали"
            this.buttonAddNode.Location = new System.Drawing.Point(0, 0);
            this.buttonAddNode.Size = new System.Drawing.Size(90, 30);
            this.buttonAddNode.Text = "Добавить";
            this.buttonAddNode.Click += new System.EventHandler(this.ButtonAddNode_Click);

            // Кнопка "Скрыть детали"
            this.buttonHideDetails.Location = new System.Drawing.Point(85, 0);
            this.buttonHideDetails.Size = new System.Drawing.Size(90, 30);
            this.buttonHideDetails.Text = "Удалить";
            this.buttonHideDetails.Click += new System.EventHandler(this.ButtonHideDetails_Click);

            // Добавление компонентов на форму
            this.Controls.Add(this.treeViewDepartments);
            this.Controls.Add(this.panelEmployee);
            this.Controls.Add(this.panelDepartment);
            this.Controls.Add(this.panelButtons);

            // Настройки формы
            this.Text = "Структура предприятия";
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void InitializeTreeView()
        {
            // Создаем корневой узел
            //Node rootNode = new Node(NodeType.DEPARTMENT, "Заводоуправление", false);

            // Создаем дочерние узлы
            /*Node department1 = new Node(NodeType.DEPARTMENT, "Отдел 1");
            Node department2 = new Node(NodeType.DEPARTMENT, "Отдел 2");

            // Добавляем дочерние узлы к корневому
            rootNode.Nodes.Add(department1);
            rootNode.Nodes.Add(department2);

            // Добавляем подузлы к дочерним узлам
            department1.AddChild(new Node(NodeType.EMPLOYEE, "Сотрудник 1"));
            department1.Nodes.Add(new Node(NodeType.EMPLOYEE, "Сотрудник 2"));
            department2.Nodes.Add(new Node(NodeType.EMPLOYEE, "Сотрудник 3"));
            department2.Nodes.Add(new Node(NodeType.EMPLOYEE, "Сотрудник 4"));*/

            // Добавляем корневой узел в TreeView
            //treeViewDepartments.Nodes.Add(rootNode);

            // Разворачиваем все узлы
            treeViewDepartments.ExpandAll();
        }
        
        
    }
}
