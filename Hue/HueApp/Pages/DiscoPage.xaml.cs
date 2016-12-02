using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class DiscoPage : Page
    {
        bool disco = true;
        private LampsPage lampsPage;

        public DiscoPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs args)
        {
            disco = false;
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            var LampGot = (LampsPage)args.Parameter;
            lampsPage = LampGot;

            disco = true;
            Disco();
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            lampsPage.mainPage.ContentFrame.GoBack();
        }

        private async void Disco()
        {
            while (disco)
            {
                    Random rnd = new Random();
                    int Hue = rnd.Next(1, 65535);
                for (int i = 0; i < lampsPage.Lamps.Count; i++)
                {
                    sendColor(i + 1, Hue);
                }
                await Task.Delay(TimeSpan.FromSeconds(0.4));
            }
        }

        private async void sendColor(int number, int Hue)
        {
            var response = await lampsPage.PageoftheMain.Lampstuff.HueSatBri(
                        number,
                        Hue,
                        254,
                        254,
                        lampsPage.PageoftheMain);
        }
    }
}
