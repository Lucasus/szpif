using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Szpif.Controls.ColumnControls;

namespace Szpif
{
    public class ControlFactory
    {
        public static SzpifControl createSzpifControl(string className, string columnName, SzpifType szpifType)
        {
            switch (className)
            {
                case "Default":
                    return new DefaultSzpifControl(columnName);
                case "State":
                    return new StateControl(columnName, szpifType);
                case "LinkControl":
                    return new LinkControl(columnName,szpifType);
                case "CheckedListBoxControl":
                    return new CheckedListBoxControl(columnName, szpifType);
                case "DateTimeControl":
                    return new DateTimeControl(columnName);
                default:
                    return new DefaultSzpifControl(columnName);
            }
        }
    }
}
