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
    public partial class AddForm : Form
    {
        AddControl addControl;

        public AddControl AddControl
        {
            get { return addControl; }
            set { addControl = value; }
        }
        public AddForm(string viewName, ViewControl control)
        {
            InitializeComponent();
            addControl = new AddControl(viewName, control);
            this.Controls.Add(addControl);
            this.Size = addControl.Size;
        }
    }
}
