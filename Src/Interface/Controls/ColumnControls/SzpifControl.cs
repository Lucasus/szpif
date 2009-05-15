using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Szpif
{
    public partial class SzpifControl : UserControl
    {
        string columnName;
        public SzpifControl(string columnName)
        {
            this.Name = columnName;
            this.columnName = columnName;
            InitializeComponent();
        }

        public SzpifControl()
        {
            InitializeComponent();
        }
        public virtual void fill(string data)
        {
            throw new NotImplementedException();
        }
        public virtual object getData()
        {
            throw new NotImplementedException();
        }
    }
}
