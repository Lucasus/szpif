using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SzpifbaseBuilder
{
    class ComputedViewColumn : ViewColumn 
    {
        ViewColumn computedFrom;
        string functionName;
        public override string getFullName()
        {
            return functionName + "(" + computedFrom.getFullName() +
                ") AS " + columnName + " ";
        }

        public ComputedViewColumn(ViewColumn cf, string functionName, string alias)
            : base(null,alias,true)
        {
            this.computedFrom = cf;
            this.functionName = functionName;
        }
    }
}
