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
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                (e.Parameter as RadioButton).IsChecked = true;
            }
            SetDefaultDate();
        }

        private void SetDefaultDate()
        {
            DatePicker.Date = new DateTimeOffset(App.date);
            DatePicker.IsTodayHighlighted = false;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            DateTime pickedTime;

            if (DatePicker.Date != null)
            {
                pickedTime = DatePicker.Date.Value.DateTime;
            }
            else
            {
                TextBlockMessage.Text = "Please select a date before saving!";
                return;
            }

            if (pickedTime.Year > App.date.Year)
            {
                TextBlockMessage.Text = "Please select a date that isn't after the current date at NASA!";
            }
            else if (pickedTime.Year == App.date.Year && pickedTime.Month > App.date.Month)
            {
                TextBlockMessage.Text = "Please select a date that isn't after the current date at NASA!";
            }
            else if (pickedTime.Year == App.date.Year && pickedTime.Month == App.date.Month && pickedTime.Day > App.date.Day)
            {
                TextBlockMessage.Text = "Please select a date that isn't after the current date at NASA!";
            }
            else
            {
                App.date = pickedTime;
                App.formattedDate = pickedTime.Year + "-" + pickedTime.Month + "-" + pickedTime.Day;
                TextBlockMessage.Text = "Date saved! Check out what occured in space on this new day!";
            }
        }
    }
}
