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
    public partial class SelectControl : UserControl
    {

        IntegratedView view;
        LinkControl linkedControl;
        DataGridViewRow selectedRow;

        public DataGridViewRow SelectedRow
        {
            get { return selectedRow; }
            set { selectedRow = value; }
        }

        public LinkControl LinkedControl
        {
            get { return linkedControl; }
            set { linkedControl = value; }
        }

        public DataGridView GridView
        {
            get { return gridView; }
            set { gridView = value; }
        }
        public SelectControl(LinkControl linkedControl)
        {
            InitializeComponent();
            this.linkedControl = linkedControl;
            gridView.Name = linkedControl.LinkType.Additional;
            view = Program.Context.ViewToGridManager.bindToView(gridView);
            this.Height = OKButton.Location.Y + OKButton.Height + 40;
            this.Width = CancelButton.Location.X + CancelButton.Width + 20;
        }
        internal void setSelectedValue(LinkControl linkControl)
        {
            int Id = (int)GridView.CurrentRow.Cells["Id"].Value;
            string Text = GridView.CurrentRow.Cells[1].Value.ToString();
            linkControl.LabelText = Text;
            linkControl.LinkedRowId = Id;
        }

        private void gridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = gridView.CurrentRow;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            setSelectedValue(linkedControl);
            ((Form)this.Parent).Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            ((Form)this.Parent).Close();
        }
    }
}
