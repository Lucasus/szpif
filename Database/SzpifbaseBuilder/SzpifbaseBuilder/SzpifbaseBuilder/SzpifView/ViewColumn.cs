using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SzpifbaseBuilder
{
    public class ViewColumn
    {
        protected string tableName;
        protected string columnName;
        private bool readOnly;

        protected bool ReadOnly
        {
            get { return readOnly; }
            set { readOnly = value; }
        }

        public ViewColumn(string tableName, string columnName, bool readOnly)
        {
            this.tableName = tableName;
            this.columnName = columnName;
            this.readOnly = readOnly;
        }
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        public string ColumnName
        {
            get { return columnName; }
            set { columnName = value; }
        }

        virtual public string getFullName()
        {
            return tableName + "1." + columnName + " ";
        }

    }
}
