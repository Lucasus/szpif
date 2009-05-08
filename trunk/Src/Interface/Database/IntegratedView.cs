using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlTypes;
using System.Data.SqlClient;
namespace Szpif
{
    public class IntegratedView
    {
        DataTable table;
        Dictionary<string, SzpifColumn> columns;

        public Dictionary<string, SzpifColumn> Columns
        {
            get { return columns; }
            set { columns = value; }
        }

        public DataTable Table
        {
            get { return table; }
            set { table = value; }
        }
        public IntegratedView()
        {
            this.table = null;
            this.columns = new Dictionary<string, SzpifColumn>();
        }


    }
}