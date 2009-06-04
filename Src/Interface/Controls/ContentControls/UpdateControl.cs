using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Szpif.Controls.ContentControls
{
    public partial class UpdateControl : UserControl
    {
        List<SzpifControl> valueBoxes;
        DataGridView gridView;
        IntegratedView view;
        ViewControl viewControl;
        ContentManager contentManager;
        int rowIndex;

        public List<SzpifControl> ValueBoxes
        {
            get { return valueBoxes; }
            set { valueBoxes = value; }
        }
        public int RowIndex
        {
            get { return rowIndex; }
            set { rowIndex = value; }
        }
        public IntegratedView View
        {
            get { return view; }
            set { view = value; }
        }
        public DataGridView GridView
        {
            get { return gridView; }
            set { gridView = value; }
        }
        
        public UpdateControl(string viewName, ViewControl viewControl)
        {
            contentManager = Program.Context.ContentManager;
            this.viewControl = viewControl;
            this.gridView = viewControl.GridView;
            this.view = viewControl.View;
            InitializeComponent();
            valueBoxes = contentManager.generateContent(this, gridView, view);
            SzpifControl last = valueBoxes[valueBoxes.Count-1];
            this.OKButton.Location = new Point(OKButton.Location.X, last.Location.Y + last.Size.Height);
            this.deleteButton.Location = new Point(deleteButton.Location.X, last.Location.Y + last.Size.Height);
            this.cancelButton.Location = new Point(cancelButton.Location.X, last.Location.Y + last.Size.Height);
            this.Height = OKButton.Location.Y + OKButton.Height + 40;
            this.Width = cancelButton.Location.X + cancelButton.Width + 20;
            if (view.Deletable == false) deleteButton.Visible = false;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            contentManager.setDataInGrid(valueBoxes, gridView, view, RowIndex);
            ((Form)this.Parent).Close();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)gridView.DataSource;
            int id = (int)gridView.Rows[RowIndex].Cells["Id"].Value;
            DataRow[] dr = dt.Select("Id = " + id);
            dr[0].Delete();
            ((Form)this.Parent).Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            ((Form)this.Parent).Close();
        }
    }
}
