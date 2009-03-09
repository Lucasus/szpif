using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinesLogic
{
    public interface IDatabase
    {
        ICollection<string> CheckLogin(string login, string password);
    }
}
