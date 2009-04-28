using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Logic
{
    public class LinkedTextBox : TextBox
    {
        int rowId;
        public int RowId
        {
            get { return rowId; }
            set { rowId = value; }
        }
        public LinkedTextBox()
            : base()
        {
        }
    }
}
