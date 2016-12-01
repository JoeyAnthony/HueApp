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
        private ObservableCollection<Lamp> Lamps = new ObservableCollection<Lamp>();
        private Command commands = new Command();

        public bool connected = false;
        public ObservableCollection<Lamp> LampsProp { get { return Lamps; } }
        public Command Lampstuff { get { return commands; } }

        public MainPage()
        {
            this.InitializeComponent();
            ContentFrame.Navigate(typeof(HomePage), this);
            HamburberBox.SelectedItem = Home;
            GetLamps();
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
            else if (User.IsSelected)
            {
                TitleTextBlock.Text = "User";
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

        public async void GetLamps()
        {
           var collection = await commands.GetAllLamps(this);
            if(collection == null)
            {
                return;
            }
            else
            {
                Lamps = collection;
            }
        }

        //private void NewAccount_Click()
        //{
        //    commands.CreateAccount("name", "HueApp", this);
        //}

        private void AllColor_Click()
        {
            foreach (Lamp l in Lamps)
            {
                commands.HueSatBri(l.Number, 100, 100, 100, this);
            }
        }
    }
}
