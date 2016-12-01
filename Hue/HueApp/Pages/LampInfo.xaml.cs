using Hue;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HueApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LampInfo : Page
    {
        private Lamp Lamp;
        private LampsPage LampsPage;

        public LampInfo()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            var LampGot = (LampsPage)args.Parameter;
            LampsPage = LampGot;
            Lamp = LampsPage.getLamp(LampsPage.currentNumber);

            if (!LampsPage.Allbutton)
                LampName.Text = Lamp.Name;
            else
                LampName.Text = "All Lamps";

            lightToggle.IsOn = Lamp.IsOn;
            lightToggle.Toggled += Switch_Click;

            HueSlider.Value = Lamp.Hue;
            SatSlider.Value = Lamp.Saturation;
            BriSlider.Value = Lamp.Brightness;

            ColorRect.Fill = new SolidColorBrush(ColorUtil.HsvToRgb(HueSlider.Value, SatSlider.Value, BriSlider.Value));
        }

        private void Switch_Click(object sender, RoutedEventArgs e)
        {
            if (!LampsPage.Allbutton)
            {
                Switch(Lamp.Number);
            }
            else
            {
                for (int i = 0; i < LampsPage.Lamps.Count; i++)
                {
                    Switch(i + 1);
                }
            }
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            if (!LampsPage.Allbutton)
            {
                sendColor(Lamp.Number);
            }
            else
            {
                for(int i = 0; i<LampsPage.Lamps.Count; i++)
                {
                    sendColor(i + 1);
                }   
            }
        }

        private async void Switch(int number)
        {
            LampsPage.Lamps[number-1].IsOn = await LampsPage.PageoftheMain.Lampstuff.LampSwitch(LampsPage.Lamps[number - 1].Number, LampsPage.Lamps[number - 1].IsOn, LampsPage.PageoftheMain);
        }


        private async void sendColor(int number)
        {
            var response = await LampsPage.PageoftheMain.Lampstuff.HueSatBri(
                    number,
                    (int)HueSlider.Value,
                    (int)SatSlider.Value,
                    (int)BriSlider.Value,
                    LampsPage.PageoftheMain);
            if (response == null)
            {
                return;
            }
            else
            {
                LampsPage.Lamps[number-1].Hue = response[0];
                LampsPage.Lamps[number - 1].Saturation = response[1];
                LampsPage.Lamps[number - 1].Brightness = response[2];

                HueSlider.Value = LampsPage.Lamps[number - 1].Hue;
                SatSlider.Value = LampsPage.Lamps[number - 1].Saturation;
                BriSlider.Value = LampsPage.Lamps[number - 1].Brightness;
            }
        }

        private void Slider_Drag(object sender, RangeBaseValueChangedEventArgs e)
        {
            ColorRect.Fill = new SolidColorBrush(ColorUtil.HsvToRgb(HueSlider.Value, SatSlider.Value, BriSlider.Value));
        }
    }
}
