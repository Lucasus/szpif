using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlTypes;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using Logic;

namespace Interface
{
    public partial class ChangeEmployeeForm : Form
    {
        int row;
        ContentManager contentManager;
        DataGridView gridView;
        List<Control> valueBoxes;

        public ChangeEmployeeForm()
        {
            row = Program.Context.ActualGridArguments.RowIndex;
            contentManager = Program.Context.ContentManager;
            InitializeComponent();
            gridView = Program.Context.ActualGridView;
            valueBoxes = contentManager.generateContent(this, gridView, Program.Context.ActualIntegratedView);
            contentManager.getDataFromGrid(valueBoxes, gridView, Program.Context.ActualIntegratedView, row);
            Control searchButton = this.Controls["searchButton"];
            searchButton.Click += new System.EventHandler(this.searchButton_Click);
        }

        protected void MainForm_Activated(object sender, System.EventArgs e)
        {
            row = Program.Context.ActualGridArguments.RowIndex;
            contentManager.getDataFromGrid(valueBoxes, gridView, Program.Context.ActualIntegratedView, row);
        }

        protected void MainForm_Closed(object sender, System.EventArgs e)
        {
        }

        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            contentManager.setDataInGrid(valueBoxes, gridView, Program.Context.ActualIntegratedView, row);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void deleteEmployeeButton_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)gridView.DataSource;
            int id = (int)gridView.Rows[row].Cells["Id"].Value;
            DataRow[] dr = dt.Select("Id = " + id);
            dr[0].Delete();
            this.Close();
        }
        private void searchButton_Click(object sender, EventArgs e)
        {
            Program.Context.FormManager.showForm("SelectPrzelozonyForm");
            SelectPrzelozonyForm form = (SelectPrzelozonyForm)Program.Context.FormManager.getForm("SelectPrzelozonyForm");
            DataGridViewRow row = form.SelectedRow;
            foreach (Control c in valueBoxes)
            {
                if (c.Name == "Przelozony")
                {
                    LinkedTextBox box = (LinkedTextBox)c;
                    box.Text = row.Cells["Name"].Value.ToString();
                    box.RowId = Int32.Parse(row.Cells["Id"].Value.ToString());
                }
            }
            //            this.Close();
        }

    }
}
