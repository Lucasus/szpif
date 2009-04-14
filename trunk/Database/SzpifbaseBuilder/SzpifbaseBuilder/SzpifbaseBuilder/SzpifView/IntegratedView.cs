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
        public IntegratedView(string viewName, List<ViewColumn> columns, ViewPermissions permissions)
        {
            this.viewName = viewName;
            this.columns = columns;
            this.permissions = permissions;
        }
    }
}
