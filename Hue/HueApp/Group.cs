using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hue
{
    class Group
    {
        List<int> LightNumbers = new List<int>();
        string Name;
        public bool IsOn = false;
        public int Brightness = 0;
        public int Hue = 0;
        public int Saturation = 0;

        public Group()
        {

        }
    }
}
