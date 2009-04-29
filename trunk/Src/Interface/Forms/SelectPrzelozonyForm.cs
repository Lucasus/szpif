using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Logic;

namespace Interface
{
    public partial class SelectPrzelozonyForm : Form
    {
        ContentManager contManager;
        BindManager bindManager;
        IntegratedView view;
        DataGridViewRow selectedRow;

        public DataGridViewRow SelectedRow
        {
            get { return selectedRow; }
            set { selectedRow = value; }
        }
        public SelectPrzelozonyForm()
        {
            InitializeComponent();
            selectPrzelozonyGridView.AllowUserToAddRows = false;
            this.selectPrzelozonyGridView.AllowUserToDeleteRows = false;
            this.selectPrzelozonyGridView.AllowUserToResizeColumns = false;
            this.selectPrzelozonyGridView.AllowUserToResizeRows = false;
            this.selectPrzelozonyGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.selectPrzelozonyGridView.RowHeadersVisible = false;

            contManager = Program.Context.ContentManager;
            bindManager = Program.Context.ViewToGridManager;


            // initializing dataGridView
            selectPrzelozonyGridView.Name = "PrzelozeniForSelect";

            // binding
            view = bindManager.bindToView(selectPrzelozonyGridView);




        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void selectPrzelozonyGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = selectPrzelozonyGridView.CurrentRow;
        }
    }
}
