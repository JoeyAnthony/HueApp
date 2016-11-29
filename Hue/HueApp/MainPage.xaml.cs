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
using Windows.UI.Text;

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
            ContentFrame.Navigate(typeof(HomePage), this);
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
                BackButton.Visibility = Visibility.Collapsed;
                ContentFrame.Navigate(typeof(HomePage), this);
            }
            else if (Settings.IsSelected)
            {
                TitleTextBlock.Text = "Settings";
                BackButton.Visibility = Visibility.Collapsed;
                ContentFrame.Navigate(typeof(SettingsPage), this);
            }
            else if (LampsBox.IsSelected)
            {
                TitleTextBlock.Text = "Lamps";
                BackButton.Visibility = Visibility.Collapsed;
                ContentFrame.Navigate(typeof(LampsPage), this);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.GoBack();
            BackButton.Visibility = Visibility.Collapsed;
        }

        public async void ErrorOccurred(int errorCode, string errorMessage)
        {
            ContentDialog dialog = new ContentDialog()
            {
                FontSize = 26,
                Title = "Error: " + errorCode.ToString(),
                MaxWidth = this.ActualWidth,
                PrimaryButtonText = "Ok",
                Content = new TextBlock
                {
                    FontSize = 15,
                    Text = errorMessage
                }
            };

            await dialog.ShowAsync();
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
