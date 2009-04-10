using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic;

namespace DatabaseLibrary
{
    class SzpifTransaction
    {
        private bool failed;

        public bool Failed
        {
            get { return failed; }
            set { failed = value; }
        }
        virtual protected void execute()
        {
        }
        public void tryExecute()
        {
            try
            {
                SzpifDatabase.Connection.Open();
                execute();
                Failed = false;
            }
            catch (Exception e)
            {
                e.ToString();
                Failed = true;
            }
            finally
            {
                SzpifDatabase.Connection.Close();
            }
        }
    }
}
