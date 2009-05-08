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
                default:
                    return new DefaultSzpifColumn();
            }
        }

    }
}
