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
    public partial class AddControl : UserControl
    {
        DataGridView gridView;
        IntegratedView view;
        ViewControl viewControl;
        List<SzpifControl> valueBoxes;

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
        public AddControl(string viewName, ViewControl viewControl)
        {
            this.View = viewControl.View;
            this.gridView = viewControl.GridView;
            this.viewControl = viewControl;
            valueBoxes = Program.Context.ContentManager.generateContent(this, gridView, view);

            InitializeComponent();
            SzpifControl last = valueBoxes[valueBoxes.Count - 1];
            this.OKButton.Location = new Point(OKButton.Location.X, last.Location.Y + last.Size.Height);
            this.CancelButton.Location = new Point(CancelButton.Location.X, last.Location.Y + last.Size.Height);
            this.Height = OKButton.Location.Y + OKButton.Height + 40;
            this.Width = CancelButton.Location.X + CancelButton.Width + 20;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Program.Context.ContentManager.addRowToView(valueBoxes, gridView, view);
            ((Form)this.Parent).Close();

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            ((Form)this.Parent).Close();
        }
    }
}
