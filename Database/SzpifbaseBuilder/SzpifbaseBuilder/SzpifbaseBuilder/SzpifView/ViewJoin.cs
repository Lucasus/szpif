using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SzpifbaseBuilder
{
    public class ViewJoin
    {
        ViewColumn masterColumn;
        ViewColumn slaveColumn;
        string table;

        public string Table
        {
            get { return table; }
            set { table = value; }
        }

        public ViewColumn MasterColumn
        {
            get { return masterColumn; }
            set { masterColumn = value; }
        }

        public ViewColumn SlaveColumn
        {
            get { return slaveColumn; }
            set { slaveColumn = value; }
        }
        public ViewJoin(string table, ViewColumn master, ViewColumn slave)
        {
            this.masterColumn = master;
            this.slaveColumn = slave;
            this.table = table;
        }
        public string getText()
        {
            return "inner join [" + table + "]" + table + "1 on "
                + masterColumn.getFullName() + " = "
                + slaveColumn.getFullName() + " ";
        }
    }
}
