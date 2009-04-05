﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public interface IDatabase
    {
        ICollection<string> CheckLogin(string login, string password, string priviliges);
		void ChangePassword(string login, string password, string newPassword, string priviliges);
    }
}