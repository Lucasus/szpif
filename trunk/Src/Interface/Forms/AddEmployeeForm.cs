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
    public partial class AddEmployeeForm : Form
    {
        DataGridView gridView;
        DataTable schema;
        List<Control> valueBoxes;
        public AddEmployeeForm()
        {
            InitializeComponent();
            gridView = Program.Context.ActualGridView;
            schema = Program.Context.ActualSchema;
            valueBoxes = Program.Context.ContentManager.generateContent(this, gridView,schema);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Program.Context.ContentManager.addRowToView(valueBoxes, gridView, schema);
            this.Close();
        }
    }
}
