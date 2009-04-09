using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseLibrary
{
    class EmptyTransaction : ITransaction
    {
        protected override void execute()
        {
        }
    }
}
