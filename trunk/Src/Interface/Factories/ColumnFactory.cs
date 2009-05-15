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
                default:
                    return new DefaultSzpifColumn();
            }
        }

    }
}
