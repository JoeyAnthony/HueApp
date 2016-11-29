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
    public sealed partial class LampsPage : Page
    {
        private Hue.MainPage mainPage;
        ObservableCollection<Lamp> Lamps = new ObservableCollection<Lamp>();

        public LampsPage()
        {
            this.InitializeComponent();
            Lamps.Add(new Hue.Lamp("Lamp1"));
            Lamps.Add(new Hue.Lamp("Lamp2"));
            Lamps.Add(new Hue.Lamp("Lamp3"));
            Lamps.Add(new Hue.Lamp("Lamp4"));
            Lamps.Add(new Hue.Lamp("Lamp5"));
            Lamps.Add(new Hue.Lamp("Lamp6"));
            Lamps.Add(new Hue.Lamp("Lamp7"));
            Lamps.Add(new Hue.Lamp("Lamp8"));
            Lamps.Add(new Hue.Lamp("Lamp9"));
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            var Mainpage = (Hue.MainPage)args.Parameter;
            mainPage = Mainpage;
        }

        private void LampButton_Click(object sender, RoutedEventArgs e)
        {
            mainPage.BackButton.Visibility = Visibility.Visible;
            mainPage.ContentFrame.Navigate(typeof(LampInfo));
        }

        private void AllLamps_Button(object sender, RoutedEventArgs e)
        {

        }
    }
}
