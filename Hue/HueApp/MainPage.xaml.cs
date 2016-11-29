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
using HueApp.Pages;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Net.Http;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Hue
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<Lamp> Lamps = new ObservableCollection<Lamp>();
        Command commands = new Command();

        public MainPage()
        {
            this.InitializeComponent();
            ContentFrame.Navigate(typeof(HomePage));
            HamburberBox.SelectedItem = Home;
        }

        private void OpenClosePane_Click(object sender, RoutedEventArgs e)
        {
            Split.IsPaneOpen = !Split.IsPaneOpen;
        }

        private void HamburgerBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Home.IsSelected)
            {
                TitleTextBlock.Text = "Home";
                ContentFrame.Navigate(typeof(HomePage));
            }
            else if (Settings.IsSelected)
            {
                TitleTextBlock.Text = "Settings";
                ContentFrame.Navigate(typeof(SettingsPage));
            }
            else if (LampsBox.IsSelected)
            {
                TitleTextBlock.Text = "Lamps";
                ContentFrame.Navigate(typeof(LampsPage));
            }
        }

        public async void GetLamps(object sender, RoutedEventArgs e)
        {
            Lamps = await commands.GetAllLamps();
        }

        private void NewAccount_Click(object sender, RoutedEventArgs e)
        {
            commands.CreateAccount("name", "HueApp");
        }

        private void AllColor_Click(object sender, RoutedEventArgs e)
        {
            foreach (Lamp l in Lamps)
            {
                commands.HueSatBri(l.Number, 100, 100, 100);
            }
        }
    }
}
