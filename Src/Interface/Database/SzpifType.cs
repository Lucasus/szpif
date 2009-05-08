using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Szpif
{
    public class SzpifType
    {
        string name;
        string type;
        string subtype;
        string schema;
        string additional;

        public string Additional
        {
            get { return additional; }
            set { additional = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Subtype
        {
            get { return subtype; }
            set { subtype = value; }
        }

        public string Schema
        {
            get { return schema; }
            set { schema = value; }
        }

    }
}
