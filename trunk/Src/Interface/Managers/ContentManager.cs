#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data.SqlTypes;
#endregion
namespace Szpif
{
    public class ContentManager
    {
        /// <summary>
        /// Generates the content for insert Form.
        /// </summary>
        /// <param name="InsertForm">The insert form.</param>
        /// <param name="grid">Grid, którego kolumny będziemy podpinać.</param>
        /// <param name="schema">Schemat widoku, który jest podpięty do datagridu.</param>
        public List<SzpifControl> generateContent(Control parent, DataGridView gridView, IntegratedView view)
        {
            List<SzpifControl> valueBoxes = new List<SzpifControl>();
            int aktY = 10;
            foreach(SzpifColumn column in view.Columns.Values)
                if (column.CanUpdate)
                {
                    SzpifControl control = column.createControl();
                    control.Location = new Point(control.Location.X, aktY);
                    aktY += control.Size.Height;
                    valueBoxes.Add(control);
                    parent.Controls.Add(control);
                }
            return valueBoxes;        
        }

        public void addRowToView(List<SzpifControl> valueBoxes, DataGridView gridView, IntegratedView view)
        {
            DataTable dt = (DataTable)gridView.DataSource;
            DataRow dr = dt.NewRow();
            foreach (SzpifControl c in valueBoxes)
            {
                    dr[c.Name] = c.getData();
            }

            Int32 last = 0;
            if(dt.Rows.Count > 0)last = (Int32)dt.Rows[dt.Rows.Count - 1]["Id"] + 1;
            dr["Id"] = last;
            dt.Rows.Add(dr);
        }

        public void setDataInGrid(List<SzpifControl> valueBoxes, DataGridView gridView, IntegratedView view, int row)
        {
            foreach (SzpifControl c in valueBoxes)
            {
                gridView.Rows[row].Cells[c.Name].Value = c.getData();
            }
        }

        public void getDataFromGrid(List<SzpifControl> valueBoxes, DataGridView gridView, IntegratedView view, int row)
        {
            foreach (SzpifControl c in valueBoxes)
                if (gridView.Columns.Contains(c.Name) && gridView.Columns[c.Name].Visible == true)
                    c.fill(gridView.Rows[row].Cells[c.Name].Value.ToString());
        }
    }
}
