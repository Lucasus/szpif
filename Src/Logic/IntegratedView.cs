using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlTypes;
using System.Data.SqlClient;
namespace Logic
{
    public class IntegratedView
    {
        SqlParameterCollection canUpdate;
        SqlParameterCollection canInsert;
        DataColumnCollection visibleColumns;
        DataTable table;

        public DataTable Table
        {
            get { return table; }
            set { table = value; }
        }

        public SqlParameterCollection CanUpdate
        {
            get { return canUpdate; }
            set { canUpdate = value; }
        }
        public SqlParameterCollection CanInsert
        {
            get { return canInsert; }
            set { canInsert = value; }
        }

        public DataColumnCollection VisibleColumns
        {
            get { return visibleColumns; }
            set { visibleColumns = value; }
        }

        public IntegratedView()
        {
            this.table = null;
            this.visibleColumns = null;
            this.canInsert = null;
        }


    }
}