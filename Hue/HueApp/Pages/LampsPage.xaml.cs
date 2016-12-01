using Hue;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class LampsPage : Page
    {
        private Hue.MainPage mainPage;
        public ObservableCollection<Lamp> Lamps = new ObservableCollection<Lamp>();
        public int currentNumber { get; set; }
        public bool Allbutton = false;

        public MainPage PageoftheMain { get { return mainPage; } }

        public LampsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            var Mainpage = (Hue.MainPage)args.Parameter;
            mainPage = Mainpage;
            Lamps = mainPage.LampsProp;

            for(int i = 0; i<Lamps.Count; i++)
            {
                Lamps[i].Color = new SolidColorBrush(ColorUtil.HsvToRgb(Lamps[i].Hue, Lamps[i].Saturation, Lamps[i].Brightness));
                All.Visibility = Visibility.Visible;
                Error.Visibility = Visibility.Collapsed;
            }
        }

        private void LampButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            int number = 0;
            Int32.TryParse(clickedButton.Name, out number);
            currentNumber = number;
            Allbutton = false;

            mainPage.BackButton.Visibility = Visibility.Visible;
            mainPage.ContentFrame.Navigate(typeof(LampInfo), this);
        }

        private void AllLamps_Button(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            currentNumber = 1;
            Allbutton = true;

            mainPage.BackButton.Visibility = Visibility.Visible;
            mainPage.ContentFrame.Navigate(typeof(LampInfo), this);
        }

        public Lamp getLamp(int number)
        {
            for(int i = 0; i<Lamps.Count; i++)
            {
                if (Lamps[i].Number == number)
                {
                    return Lamps[i];
                }
            }
            return new Lamp();
        }

        private async void Refresh_Click(object sender, RoutedEventArgs e)
        {
            mainPage.GetLamps();
            Lamps = mainPage.LampsProp;

            await Task.Delay(TimeSpan.FromSeconds(0.3));

            mainPage.ContentFrame.GoBack();
            mainPage.ContentFrame.GoForward();
        }
    }
}
