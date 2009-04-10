using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Interface
{
    public partial class ChangeEmployeeForm : Form
    {
        int row;
        DataGridView gridView;
        public ChangeEmployeeForm()
        {
            InitializeComponent();
            row = Program.Context.ActualGridArguments.RowIndex;
            gridView = Program.Context.ActualGridView;
            this.LoginTextBox.Text = gridView.Rows[row].Cells["Login"].Value.ToString();

        }

        protected void MainForm_Activated(object sender, System.EventArgs e)
        {
            row = Program.Context.ActualGridArguments.RowIndex;
            gridView = Program.Context.ActualGridView;
            this.LoginTextBox.Text = gridView.Rows[row].Cells["Login"].Value.ToString();
        }

        protected void MainForm_Closed(object sender, System.EventArgs e)
        {
        }

        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            gridView.Rows[row].Cells["Login"].Value = LoginTextBox.Text;
        }
    }
}
