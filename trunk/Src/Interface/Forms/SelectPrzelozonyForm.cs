using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Interface
{
    public partial class SelectPrzelozonyForm : Form
    {
        public SelectPrzelozonyForm()
        {
            InitializeComponent();
            selectPrzelozonyGridView.AllowUserToAddRows = false;
            this.selectPrzelozonyGridView.AllowUserToDeleteRows = false;
            this.selectPrzelozonyGridView.AllowUserToResizeColumns = false;
            this.selectPrzelozonyGridView.AllowUserToResizeRows = false;
            this.selectPrzelozonyGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.selectPrzelozonyGridView.RowHeadersVisible = false;

        }
    }
}
