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
        public int currentNumber { get; set; }

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
        }

        private void LampButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            int number = 0;
            Int32.TryParse(clickedButton.Name, out number);
            currentNumber = number;

            mainPage.BackButton.Visibility = Visibility.Visible;
            mainPage.ContentFrame.Navigate(typeof(LampInfo), this);
        }

        private void AllLamps_Button(object sender, RoutedEventArgs e)
        {

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
    }
}
