using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hue
{
    class Lamp
    {
        private int Number;
        public string Name;

        public Lamp()
        {
        }

        public Lamp(int num, string name)
        {
            Number = num;
            Name = name;
        }
    }
}
