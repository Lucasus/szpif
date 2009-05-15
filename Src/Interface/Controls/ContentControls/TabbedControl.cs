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
        public TabbedControl(List<TabPage> pages) : base()
        {
            InitializeComponent();
            foreach (TabPage page in pages)
            {
                tabControl.TabPages.Add(page);
//                this.tabControl
            }
        }
    }
}
