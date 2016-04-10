using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TodayInSpace;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

namespace TodayInSpace
{
    public sealed partial class Shell : Page
    {
        public Shell()
        {
            InitializeComponent();
        }

        public Shell(Frame frame)
        {
            InitializeComponent();
            SplitViewMain.Content = frame;
            (SplitViewMain.Content as Frame).Navigate(typeof(MainPage), AstronomyPictureRadioButton);
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            SplitViewMain.IsPaneOpen = !SplitViewMain.IsPaneOpen;
        }

        private void AstronomyPictureRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SplitViewMain.IsPaneOpen = false;
            if (SplitViewMain.Content != null)
            {
                (SplitViewMain.Content as Frame).Navigate(typeof(MainPage), AstronomyPictureRadioButton);
            }
        }

        private void LocationImageryRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SplitViewMain.IsPaneOpen = false;
            (SplitViewMain.Content as Frame).Navigate(typeof(LocationImageryPage), LocationImageryRadioButton);
        }

        private void MarsRoverRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SplitViewMain.IsPaneOpen = false;
            (SplitViewMain.Content as Frame).Navigate(typeof(MarsRoverPage), MarsRoverRadioButton);
        }

        private void SettingsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SplitViewMain.IsPaneOpen = false;
            (SplitViewMain.Content as Frame).Navigate(typeof(SettingsPage), SettingsRadioButton);
        }
    }
}
