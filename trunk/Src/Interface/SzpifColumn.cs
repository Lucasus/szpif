using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Szpif
{
    public class SzpifColumn
    {
        string name;
        SzpifType type;
        bool canInsert;
        bool canUpdate;
        bool canView;

        public bool CanInsert
        {
            get { return canInsert; }
            set { canInsert = value; }
        }

        public bool CanUpdate
        {
            get { return canUpdate; }
            set { canUpdate = value; }
        }

        public bool CanView
        {
            get { return canView; }
            set { canView = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public SzpifType Type
        {
            get { return type; }
            set { type = value; }
        }

        public virtual Control createControl()
        {
            return null;
        }

    }
}
