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
    public partial class DefaultSzpifControl : SzpifControl
    {
        public DefaultSzpifControl(string columnName) : base(columnName)
        {
            InitializeComponent();
            this.Name = columnName;

            this.ColumnNameLabel.Text = columnName;
        }

        public override void fill(string data)
        {
            this.ColumnValue.Text = data;
        }

        public override object getData()
        {
            if(this.ColumnValue.Text != "")
                return this.ColumnValue.Text;
            else return DBNull.Value;

        }

    }
}
