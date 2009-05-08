using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Szpif;

namespace Interface
{
    public partial class AddEmployeeForm : Form
    {
        DataGridView gridView;
        IntegratedView view;
        List<Control> valueBoxes;
        public AddEmployeeForm()
        {
            InitializeComponent();
            gridView = Program.Context.ActualGridView;
            view = Program.Context.ActualIntegratedView;
            valueBoxes = Program.Context.ContentManager.generateContent(this, gridView,view);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Program.Context.ContentManager.addRowToView(valueBoxes, gridView, view);
            this.Close();
        }
    }
}
