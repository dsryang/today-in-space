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

namespace TodayInSpace
{
    public sealed partial class MarsRoverPage : Page
    {
        public MarsRoverPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                (e.Parameter as RadioButton).IsChecked = true;
            }
        }

        private void ButtonFHAZ_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MarsRoverResultPage), "FHAZ");
        }

        private void ButtonRHAZ_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MarsRoverResultPage), "RHAZ");
        }

        private void ButtonCHEMCAM_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MarsRoverResultPage), "CHEMCAM");
        }

        private void ButtonMAHLI_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MarsRoverResultPage), "MAHLI");
        }

        private void ButtonNAVCAM_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MarsRoverResultPage), "NAVCAM");
        }
    }
}
