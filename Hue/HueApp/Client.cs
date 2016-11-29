using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hue
{
    class Client
    {
        private static string UName = "0ec36b5aca20351a9c5230e857b351c";
        private static string IPadress = "localhost";
        private static int PortNum = 8000;

        public string UserName { get { return UName; } set { UName = value; } }
        public string IP { get { return IPadress; } set { IPadress = value; } }
        public int Port { get { return PortNum; } set { PortNum = value; } }

        public Client()
        {

        }
    }
}
