using System.ComponentModel;

namespace KrasOctTest;

partial class RenameForm
{

    private IContainer components = null;
    private TextBox textBoxName;
    private Button buttonOK;
    private Button buttonCancel;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.textBoxName = new TextBox();
            this.buttonOK = new Button();
            this.buttonCancel = new Button();
            

            this.SuspendLayout();

            this.textBoxName.Location = new System.Drawing.Point(12, 12);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(260, 20);
            this.textBoxName.TabIndex = 0;

            this.buttonOK.Location = new System.Drawing.Point(116, 50);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "ОК";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new EventHandler(this.ButtonOK_Click);

            this.buttonCancel.Location = new System.Drawing.Point(197, 50);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new EventHandler(this.ButtonCancel_Click);

            this.ClientSize = new System.Drawing.Size(284, 85);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Name = "NameForm";
            this.Text = "Введите наименование";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

    #endregion
}