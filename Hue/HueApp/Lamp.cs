using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hue
{
    class Lamp
    {
        public int Number;
        public string Name;
        public string ModelID;
        public string UniqueID;
        public string Type;
        public bool IsOn = false;
        public int Brightness = 0;
        public int Hue = 0;
        public int Saturation = 0;

        public Lamp()
        {
        }

        public Lamp(string name)
        {
            Name = name;
        }

        public Lamp(int num, string name)
        {
            Number = num;
            Name = name;
        }

        public Lamp(int num, bool on, int bri, int hue, int sat)
        {
            Number = num;
            IsOn = on;
            Brightness = bri;
            Hue = hue;
            Saturation = sat;
        }

        //private void Switch_Click(object sender, RoutedEventArgs e)
        //{
        //    commands.LampSwitch(1, false);
        //}

        //private void Color_Click(object sender, RoutedEventArgs e)
        //{
        //    commands.HueSatBri(1, 100, 100, 100);
        //}
    }
}
