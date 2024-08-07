﻿using System;
using System.Windows.Forms;
using KrasOctTest.Data;
using KrasOctTest.TreeComponents;
using Microsoft.Extensions.DependencyInjection;

namespace KrasOctTest;

public partial class NodeCreateForm : Form
{
    private Node _currentNode;
  
    private ITreeNodeRepository _treeNodeRepository;
    
    private Label label1;
    private ComboBox comboBox1;
    private Label label2;
    private TextBox textBox1;
    private Button buttonOK;
    private Button buttonCancel;
    
    public NodeCreateForm(ITreeNodeRepository treeNodeRepository, MainForm mainForm)
    {
        _treeNodeRepository = treeNodeRepository;
        InitializeComponent();
        _currentNode = mainForm.CurrentNode;
        
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
    }
    
    private void InitializeComponent()
    {
            this.label1 = new Label();
            this.comboBox1 = new ComboBox();
            this.label2 = new Label();
            this.textBox1 = new TextBox();
            this.buttonOK = new Button();
            this.buttonCancel = new Button();
            this.SuspendLayout();

            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Тип ветки";

            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Сотрудник",
            "Отдел"});
            this.comboBox1.Location = new System.Drawing.Point(100, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(172, 21);
            this.comboBox1.TabIndex = 1;

            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Наименование";

            this.textBox1.Location = new System.Drawing.Point(100, 39);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(172, 20);
            this.textBox1.TabIndex = 3;

            this.buttonOK.Location = new System.Drawing.Point(116, 65);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);

            this.buttonCancel.Location = new System.Drawing.Point(197, 65);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 101);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Name = "Добавление ветки";
            this.Text = "Добавление ветки";
            this.ResumeLayout(false);
            this.PerformLayout();
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
        if (this.textBox1.Text == "")
        {
            MessageBox.Show("Введите наименование", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        } 
        if (this.comboBox1.SelectedIndex == -1)
        {
            MessageBox.Show("Выберите тип ветки", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        _treeNodeRepository.CreateNode(this.textBox1.Text, _currentNode, (NodeType)this.comboBox1.SelectedIndex);
        
        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    { 
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }


}
