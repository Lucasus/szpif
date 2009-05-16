using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Szpif.Forms;

namespace Szpif.Controls.ContentControls
{
    public partial class ViewControl : UserControl
    {
        string viewName;
        IntegratedView view;
        AddForm addForm;
        UpdateForm updateForm;

        public IntegratedView View
        {
            get { return view; }
            set { view = value; }
        }

        public DataGridView GridView
        {
            get { return gridView; }
        }


        public ViewControl(string viewName, int maxHeight)
        {
            InitializeComponent();
            this.gridView.Name = this.viewName = viewName;
            this.Edit.Text = "Edytuj...";
            this.Edit.UseColumnTextForButtonValue = true;
            view = Program.Context.ViewToGridManager.bindToView(gridView);
            addForm = FormFactory.createAddForm(viewName, this);
            updateForm = FormFactory.createUpdateForm(viewName, this);
            this.gridView.Height = maxHeight - 50;
//            this.
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            
            ((AddControl)addForm.Controls[0]).View = view;
            ((AddControl)addForm.Controls[0]).GridView = gridView;
            addForm.ShowDialog();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Program.Context.ViewToGridManager.updateView(gridView);
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            view = Program.Context.ViewToGridManager.reconnect(gridView);
        }

        private void gridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != gridView.Columns["Edit"].Index) return;
            ((UpdateControl)updateForm.Controls[0]).View = view;
            ((UpdateControl)updateForm.Controls[0]).GridView = gridView;
            ((UpdateControl)updateForm.Controls[0]).RowIndex = e.RowIndex;
            updateForm.ShowDialog();
        }

        private void gridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn column = this.gridView.Columns[e.ColumnIndex];
            if (view.Columns.ContainsKey(column.Name) && column.Name != "Edit")
                e.Value = view.Columns[column.Name].valueToGridString(e.Value.ToString());            
        }

        private void ViewControl_SizeChanged(object sender, EventArgs e)
        {
            this.gridView.Height = this.Height - 52;
            this.addButton.Location = new Point(this.addButton.Location.X, this.gridView.Height + 5);
            this.saveButton.Location = new Point(this.saveButton.Location.X, this.gridView.Height + 5);
            this.refreshButton.Location = new Point(this.refreshButton.Location.X, this.gridView.Height + 5);
        }
    }
}
