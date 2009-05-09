using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Szpif.Controls.ContentControls;

namespace Szpif.Forms
{
    public partial class UpdateForm : Form
    {
        UpdateControl updateControl;
        ContentManager contentManager;
        public UpdateControl UpdateControl
        {
            get { return updateControl; }
            set { updateControl = value; }
        }
        public UpdateForm(string viewName, ViewControl control)
        {
            contentManager = Program.Context.ContentManager;
            updateControl = new UpdateControl(viewName, control);
            this.Controls.Add(updateControl);
            InitializeComponent();
            this.Size = updateControl.Size;
        }

        private void UpdateForm_Shown(object sender, EventArgs e)
        {
            contentManager.getDataFromGrid(updateControl.ValueBoxes, 
                updateControl.GridView, updateControl.View, updateControl.RowIndex);
        }
    }
}
