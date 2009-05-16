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
            foreach (Control c in controls)
            {
                TabPage tabPage = new TabPage(c.Name);
                tabPage.Padding = new System.Windows.Forms.Padding(3);
                tabPage.UseVisualStyleBackColor = true;
                tabPage.Controls.Add(c);
                tabControl.TabPages.Add(tabPage);
            }
        }

        private void TabbedControl_SizeChanged(object sender, EventArgs e)
        {
            this.tabControl.Height = this.Height;
            foreach (Control c in controls)
            {
                c.Height = this.Height - 28;
            }
        }
    }
}
