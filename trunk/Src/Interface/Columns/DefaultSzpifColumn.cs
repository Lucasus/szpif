using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Szpif
{
    class DefaultSzpifColumn : SzpifColumn
    {
        public override SzpifControl createControl()
        {
            return ControlFactory.createSzpifControl("Default", this.Name, this.Type);
        }

        public override DataGridViewColumn createDataGridViewColumn()
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = this.Name;
            column.DataPropertyName = this.Name;
            if (this.Name == "Id") column.Visible = false;
            if (!CanUpdate || this.Name == "Id")
            {
                column.ReadOnly = true;
                DataGridViewCellStyle helpStyle = new DataGridViewCellStyle();
                helpStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                column.DefaultCellStyle = helpStyle;
            }
            if (this.Name != "Id")
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            else
                column.Width = 20;

            if (!CanView)
                column.Visible = false;

            return column;
        }
        public override string valueToGridString(string value)
        {
            return value;
        }
    }
}
