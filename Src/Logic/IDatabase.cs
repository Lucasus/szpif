using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public interface IDatabase
    {
        bool                CheckLogin(string login, string password);
		void                ChangePassword(string login, string password, string newPassword);
        void                setupConnectionParameters(string username, string password);
        ICollection<string> getUserPermissions();
    }
}
