using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

namespace TodayInSpace
{
    public sealed partial class LocationImageryResultPage : Page
    {
        Geopoint mapLocationPoint;
        Imagery locationImagery;

        public LocationImageryResultPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            ShowMessage("Loading...");

            if (e.Parameter != null)
            {
                mapLocationPoint = e.Parameter as Geopoint;
                await LoadImage();

                var bitmap = new BitmapImage(new Uri(locationImagery.url));
                bitmap.ImageFailed += (s, ex) => ShowMessage("An error occurred while loading the image.");
                bitmap.ImageOpened += (s, ex) => ShowImage();
                ImageLocationImagery.Source = bitmap;
            }
            else
            {
                ShowMessage("An error occurred while loading the image.");
            }
        }

        private void ButtonSearchAgain_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void ShowMessage(string message)
        {
            TextBlockMessage.Text = message;
            TextBlockMessage.Visibility = Visibility.Visible;
            ImageLocationImagery.Visibility = Visibility.Collapsed;
        }

        private void ShowImage()
        {
            TextBlockMessage.Visibility = Visibility.Collapsed;
            ImageLocationImagery.Visibility = Visibility.Visible;
        }

        private async Task LoadImage()
        {
            string longitude = mapLocationPoint.Position.Longitude.ToString();
            string latitude = mapLocationPoint.Position.Latitude.ToString();

            Uri uri = new Uri("https://api.nasa.gov/planetary/earth/imagery?api_key=" + App.nasaApiKey + "&lon=" + longitude + "&lat=" + latitude + "&date=" + App.formattedDate);

            HttpClient httpClient = new HttpClient();

            HttpResponseMessage response = await httpClient.GetAsync(uri);
            string jsonString = await response.Content.ReadAsStringAsync();

            locationImagery = JsonConvert.DeserializeObject<Imagery>(jsonString);
        }
    }
}
