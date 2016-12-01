using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace Hue
{
    public class Lamp
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
        public SolidColorBrush Color { get; set; }

        public Lamp()
        {
        }

        public Lamp(string name, int number)
        {
            Name = name;
            Number = number;
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


    }
}
