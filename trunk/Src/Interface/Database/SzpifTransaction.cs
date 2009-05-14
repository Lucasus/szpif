using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szpif;
using System.Windows.Forms;

namespace Szpif
{
    public class SzpifTransaction
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
                MessageBox.Show(e.ToString());
                Failed = true;
            }
            finally
            {
                SzpifDatabase.Connection.Close();
            }
        }
    }
}
