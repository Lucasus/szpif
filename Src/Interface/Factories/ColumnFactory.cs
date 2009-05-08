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
                    return new SzpifColumn();
                case "Link":
                    return new SzpifColumn();
                case "CheckedListBox":
                    return new SzpifColumn();
                default:
                    return new SzpifColumn();
            }
        }

    }
}
