using Hue;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
            LampName.Text = Lamp.Name;

            lightToggle.IsOn = Lamp.IsOn;
            lightToggle.Toggled += Switch_Click;

            HueSlider.Value = Lamp.Hue;
            SatSlider.Value = Lamp.Saturation;
            BriSlider.Value = Lamp.Brightness;
        }

        private void SetSwitch()
        {
        }

        private async void Switch_Click(object sender, RoutedEventArgs e)
        {
            Lamp.IsOn = await LampsPage.PageoftheMain.Lampstuff.LampSwitch(Lamp.Number, Lamp.IsOn, LampsPage.PageoftheMain);
        }

        private async void sendButton_Click(object sender, RoutedEventArgs e)
        {
            var response = await LampsPage.PageoftheMain.Lampstuff.HueSatBri(
                Lamp.Number,
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
                Lamp.Hue = response[0];
                Lamp.Saturation = response[1];
                Lamp.Brightness = response[2];

                HueSlider.Value = Lamp.Hue;
                SatSlider.Value = Lamp.Saturation;
                BriSlider.Value = Lamp.Brightness;
            }
        }
    }
}
