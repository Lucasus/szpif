using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Szpif.Controls.ContentControls;
using System.Xml;

namespace Szpif
{
    public partial class SelectForm : Form
    {
        SelectControl selectControl;
        public SelectForm(LinkControl linkedControl)
        {
            InitializeComponent();
            selectControl = new SelectControl(linkedControl);
            this.Controls.Add(selectControl);
            this.Size = selectControl.Size;
        }

    }
}
