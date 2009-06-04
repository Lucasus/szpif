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
    public partial class TabbedControl : UserControl
    {
        List<Control> controls;
        public TabbedControl(List<Control> controls)
            : base()
        {
            InitializeComponent();
            this.controls = controls;
            this.tabControl.Margin = new Padding(0, 2, 0, 0);
            this.tabControl.Padding = new Point(0, 0);
            foreach (Control c in controls)
            {
                TabPage tabPage = new TabPage(c.Name);
                tabPage.Padding = new System.Windows.Forms.Padding(0,2,0,0);
                tabPage.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
                tabPage.UseVisualStyleBackColor = true;
                tabPage.Controls.Add(c);
                tabControl.TabPages.Add(tabPage);
            }
        }

        private void TabbedControl_SizeChanged(object sender, EventArgs e)
        {
            this.tabControl.Height = this.Height;
            this.tabControl.Width = this.Width;
            foreach (Control c in controls)
            {
                c.Location = new Point(this.Location.X-10, c.Location.Y);
                c.Left = 0;
                this.Margin = new Padding(0, 2, 0, 0);
                c.Margin = new Padding(0, 2, 0, 0);
                c.Height = this.Height - 28;
            }
        }
    }
}
