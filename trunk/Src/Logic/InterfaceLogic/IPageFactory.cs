using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace InterfaceLogic
{
    public interface IPageFactory
    {
         TabPage createTabPage(string kind);
    }
}
