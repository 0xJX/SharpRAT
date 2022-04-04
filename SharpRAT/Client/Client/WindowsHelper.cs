using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Client
{
    internal class WindowsHelper
    {
        public static string GetUsername()
        {
            return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }
    }
}
