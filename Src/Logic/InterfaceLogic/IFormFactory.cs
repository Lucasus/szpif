using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Logic
{
    public interface IFormFactory
    {
        Form createNewForm(string p);   
    }
}
