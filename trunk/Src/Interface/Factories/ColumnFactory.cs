using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szpif
{
    public class ColumnFactory
    {
        public static SzpifColumn createSzpifColumn(SzpifType type)
        {
            switch (type.Subtype)
            {
                case "Default":
                    return new DefaultSzpifColumn();
                case "Link":
                    return new LinkColumn();
                case "CheckedListBox":
                    return new CheckedListBoxColumn();
                case "SqlDateTime":
                    return new DateTimeColumn();
                case "Project State":
                    return new StateColumn();
                case "Task State":
                    return new StateColumn();
                default:
                    return new DefaultSzpifColumn();
            }
        }

    }
}
