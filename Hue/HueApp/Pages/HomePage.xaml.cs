using System;
using System.Collections.Generic;
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
    public sealed partial class HomePage : Page
    {
        private Hue.MainPage mainPage;

        public HomePage()
        {
            this.InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            var Mainpage = (Hue.MainPage)args.Parameter;
            mainPage = Mainpage;
        }

        private void Joey_Click(object sender, RoutedEventArgs e)
        {
            mainPage.BackButton.Visibility = Visibility.Visible;
            mainPage.ContentFrame.Navigate(typeof(JoeyInfoPage));
        }

        private void Cas_Click(object sender, RoutedEventArgs e)
        {
            mainPage.BackButton.Visibility = Visibility.Visible;
            mainPage.ContentFrame.Navigate(typeof(CasInfoPage));
        }
    }
}
