using System.ComponentModel;

namespace KrasOctTest;

partial class InputForm
{
    private System.ComponentModel.IContainer components = null;
    private TextBox textBoxFirstName;
    private TextBox textBoxLastName;
    private TextBox textBoxPatronymic;
    private Button buttonOk;
    private Button buttonCancel;
    private Label labelFirstName;
    private Label labelLastName;
    private Label labelPatronymic;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.textBoxFirstName = new System.Windows.Forms.TextBox();
        this.textBoxLastName = new System.Windows.Forms.TextBox();
        this.textBoxPatronymic = new System.Windows.Forms.TextBox();
        this.buttonOk = new System.Windows.Forms.Button();
        this.buttonCancel = new System.Windows.Forms.Button();
        this.labelFirstName = new System.Windows.Forms.Label();
        this.labelLastName = new System.Windows.Forms.Label();
        this.labelPatronymic = new System.Windows.Forms.Label();
        this.SuspendLayout();

        this.textBoxFirstName.Location = new System.Drawing.Point(100, 10);
        this.textBoxFirstName.Name = "textBoxFirstName";
        this.textBoxFirstName.Size = new System.Drawing.Size(150, 20);
        this.textBoxFirstName.TabIndex = 0;

        this.textBoxLastName.Location = new System.Drawing.Point(100, 40);
        this.textBoxLastName.Name = "textBoxLastName";
        this.textBoxLastName.Size = new System.Drawing.Size(150, 20);
        this.textBoxLastName.TabIndex = 1;

        this.textBoxPatronymic.Location = new System.Drawing.Point(100, 70);
        this.textBoxPatronymic.Name = "textBoxPatronymic";
        this.textBoxPatronymic.Size = new System.Drawing.Size(150, 20);
        this.textBoxPatronymic.TabIndex = 2;

        this.buttonOk.Location = new System.Drawing.Point(40, 100);
        this.buttonOk.Name = "buttonOk";
        this.buttonOk.Size = new System.Drawing.Size(75, 28);
        this.buttonOk.TabIndex = 3;
        this.buttonOk.Text = "OK";
        this.buttonOk.UseVisualStyleBackColor = true;
        this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);

        this.buttonCancel.Location = new System.Drawing.Point(175, 100);
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Size = new System.Drawing.Size(75, 28);
        this.buttonCancel.TabIndex = 4;
        this.buttonCancel.Text = "Отмена";
        this.buttonCancel.UseVisualStyleBackColor = true;
        this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);

        this.labelFirstName.AutoSize = true;
        this.labelFirstName.Location = new System.Drawing.Point(10, 13);
        this.labelFirstName.Name = "labelFirstName";
        this.labelFirstName.Size = new System.Drawing.Size(29, 13);
        this.labelFirstName.TabIndex = 5;
        this.labelFirstName.Text = "Имя";

        this.labelLastName.AutoSize = true;
        this.labelLastName.Location = new System.Drawing.Point(10, 43);
        this.labelLastName.Name = "labelLastName";
        this.labelLastName.Size = new System.Drawing.Size(56, 13);
        this.labelLastName.TabIndex = 6;
        this.labelLastName.Text = "Фамилия";

        this.labelPatronymic.AutoSize = true;
        this.labelPatronymic.Location = new System.Drawing.Point(10, 73);
        this.labelPatronymic.Name = "labelPatronymic";
        this.labelPatronymic.Size = new System.Drawing.Size(54, 13);
        this.labelPatronymic.TabIndex = 7;
        this.labelPatronymic.Text = "Отчество";

        this.ClientSize = new System.Drawing.Size(284, 131);
        this.Controls.Add(this.labelPatronymic);
        this.Controls.Add(this.labelLastName);
        this.Controls.Add(this.labelFirstName);
        this.Controls.Add(this.buttonCancel);
        this.Controls.Add(this.buttonOk);
        this.Controls.Add(this.textBoxPatronymic);
        this.Controls.Add(this.textBoxLastName);
        this.Controls.Add(this.textBoxFirstName);
        this.Name = "InputForm";
        this.Text = "Добавить сотрудника";
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
