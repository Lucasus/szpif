using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
	public class Logger
	{
		
	
		public Logger()
		{
			
		}
		
		public void checkLogin(String UserName, String Password)
		{
			if(UserName == null || UserName.Length == 0 || Password == null) throw new ArgumentException();
			
		}
	}
}
