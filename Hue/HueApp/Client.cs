using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hue
{
    public class Client
    {
        private static string UName = "No Username";
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
