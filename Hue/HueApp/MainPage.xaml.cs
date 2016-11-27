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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Hue
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<Lamp> Lamps = new ObservableCollection<Lamp>();
        public MainPage()
        {
            this.InitializeComponent();
            ContentFrame.Navigate(typeof(HomePage));

            Lamps.Add(new Lamp((Lamps.Count + 1), "Lamp " + (Lamps.Count + 1)));
            Lamps.Add(new Lamp((Lamps.Count + 1), "Lamp " + (Lamps.Count + 1)));
            Lamps.Add(new Lamp((Lamps.Count + 1), "Lamp " + (Lamps.Count + 1)));
            Lamps.Add(new Lamp((Lamps.Count + 1), "Lamp " + (Lamps.Count + 1)));
            Lamps.Add(new Lamp((Lamps.Count + 1), "Lamp " + (Lamps.Count + 1)));
        }

        private void OpenClosePane_Click(object sender, RoutedEventArgs e)
        {
            Split.IsPaneOpen = !Split.IsPaneOpen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Lamps.Add(new Lamp((Lamps.Count + 1), "Lamp " + (Lamps.Count + 1)));
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
    }
}
