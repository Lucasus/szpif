using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic;

namespace DatabaseLibrary
{
    abstract class ITransaction
    {
        private bool failed;

        public bool Failed
        {
            get { return failed; }
            set { failed = value; }
        }
        abstract  protected void execute();
        public void tryExecute()
        {
            try
            {
                SzpifDatabase.Connection.Open();
                execute();
                Failed = false;
            }
            catch (Exception)
            {
                Failed = true;
            }
            finally
            {
                SzpifDatabase.Connection.Close();
            }
        }
    }
}
