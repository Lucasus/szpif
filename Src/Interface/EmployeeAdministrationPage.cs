using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Interface
{
    [Designer(typeof(TabPage))]
    [ToolboxBitmap(typeof(TabPage))]
    public partial class EmployeeAdministrationPage : TabPage
    {
        public EmployeeAdministrationPage(string text) : base(text)
        {
            InitializeComponent();
        }
    }
}
