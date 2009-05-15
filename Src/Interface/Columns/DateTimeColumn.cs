using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Szpif
{
    class DateTimeColumn : SzpifColumn
    {
        public override SzpifControl createControl()
        {
            return ControlFactory.createSzpifControl("DateTimeControl", this.Name, this.Type);
        }

        public override DataGridViewColumn createDataGridViewColumn()
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = this.Name;
            column.DataPropertyName = this.Name;
            column.ReadOnly = true;
            DataGridViewCellStyle helpStyle = new DataGridViewCellStyle();
            helpStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            column.DefaultCellStyle = helpStyle;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            if (!CanView)
                column.Visible = false;
            return column;
        }

        public override string valueToGridString(string value)
        {
            DateTime d = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss",null);
            return d.ToLongDateString() ;
        }
    }
}
