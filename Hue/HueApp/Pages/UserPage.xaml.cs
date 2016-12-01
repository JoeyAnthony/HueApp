
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
    public sealed partial class SettingsPage : Page
    {
        private Hue.MainPage mainPage;

        public SettingsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            var Mainpage = (Hue.MainPage)args.Parameter;
            mainPage = Mainpage;
            UserText.Text = mainPage.Lampstuff.ClientInfo.UserName;
            if (!mainPage.connected)
            {
                Error1.Visibility = Visibility.Visible;
                Error2.Visibility = Visibility.Visible;
                ConnectButton.Visibility = Visibility.Visible;
                UserText.Visibility = Visibility.Collapsed;
                text1.Visibility = Visibility.Collapsed;
            }
            else
            {
                Error1.Visibility = Visibility.Collapsed;
                Error2.Visibility = Visibility.Collapsed;
                ConnectButton.Visibility = Visibility.Collapsed;
                UserText.Visibility = Visibility.Visible;
                text1.Visibility = Visibility.Visible;
            }
        }

        public async void Login(object sender, RoutedEventArgs e)
        {
            if(await mainPage.Lampstuff.CreateAccount("name", "HueApp", mainPage))
            {
                UserText.Text = mainPage.Lampstuff.ClientInfo.UserName;

                mainPage.connected = true;
                Error1.Visibility = Visibility.Collapsed;
                Error2.Visibility = Visibility.Collapsed;
                ConnectButton.Visibility = Visibility.Collapsed;
                UserText.Visibility = Visibility.Visible;
                text1.Visibility = Visibility.Visible;
                mainPage.GetLamps();
            }
            
        }
    }
}
