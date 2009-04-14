using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management;

namespace SzpifbaseBuilder
{
    public class IntegratedView
    {
        string viewName;
        List<ViewColumn> columns;
        ViewPermissions permissions;
        List<string> tableNames;
        List<ViewJoin> joins;

        internal List<ViewJoin> Joins
        {
            get { return joins; }
            set { joins = value; }
        }

        public List<string> TableNames
        {
            get { return tableNames; }
            set { tableNames = value; }
        }

        public List<ViewColumn> Columns
        {
            get { return columns; }
            set { columns = value; }
        }

        public ViewPermissions Permissions
        {
            get { return permissions; }
            set { permissions = value; }
        }

        public string ViewName
        {
            get { return viewName; }
            set { viewName = value; }
        }

        
        public IntegratedView(string viewName, List<ViewColumn> columns, 
            ViewPermissions permissions, List<string> tableNames,
            List<ViewJoin> joins)
        {
            this.joins = joins;
            this.viewName = viewName;
            this.columns = columns;
            this.permissions = permissions;
            this.tableNames = tableNames;
        }

        internal void generate()
        {
            throw new NotImplementedException();
        }
    }
}
