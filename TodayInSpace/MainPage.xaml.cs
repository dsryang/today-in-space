using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class MainPage : Page
    {
        private AstronomyPicture apod;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                (e.Parameter as RadioButton).IsChecked = true;
            }

            await LoadAPOD();
            ShowAPOD();
        }

        private async Task LoadAPOD()
        {
            ShowMessage("Loading...");

            Uri uri = new Uri("https://api.nasa.gov/planetary/apod?api_key=" + App.nasaApiKey + "&date=" + App.formattedDate);
            HttpClient httpClient = new HttpClient();

            HttpResponseMessage response = await httpClient.GetAsync(uri);
            string jsonString = await response.Content.ReadAsStringAsync();

            apod = JsonConvert.DeserializeObject<AstronomyPicture>(jsonString);
        }

        private void ShowAPOD()
        {
            if (apod.media_type == "image")
            {
                var bitmap = new BitmapImage(new Uri(apod.url));
                bitmap.ImageFailed += (s, ex) => ShowMessage("An error occurred while loading the image.");
                bitmap.ImageOpened += (s, ex) => ShowImage();

                ImageAstronomyPicture.Source = bitmap;
            }
            else if (apod.media_type == "video")
            {
                string youtubeId = apod.url.Substring(apod.url.IndexOf("/embed/") + 7);
                youtubeId = youtubeId.Substring(0, youtubeId.IndexOf("?"));

                var bitmap = new BitmapImage(new Uri("http://img.youtube.com/vi/" + youtubeId + "/0.jpg"));
                bitmap.ImageFailed += (s, ex) => ShowMessage("An error occurred while loading the video thumbnail.");
                bitmap.ImageOpened += (s, ex) => ShowImage();

                ImageAstronomyPicture.Source = bitmap;
            }
        }

        private void ShowMessage(string message)
        {
            TextBlockMessage.Text = message;
            TextBlockMessage.Visibility = Visibility.Visible;
            ImageAstronomyPicture.Visibility = Visibility.Collapsed;
        }

        private void ShowImage()
        {
            TextBlockMessage.Visibility = Visibility.Collapsed;
            ImageAstronomyPicture.Visibility = Visibility.Visible;

            if (apod.media_type == "video")
            {
                TextBlockTitle.Text = "Video: " + apod.title;
                TextBlockCopyright.Visibility = Visibility.Visible;
                TextBlockCopyright.Text = "Click on the image to play the video";
            }
            else
            {
                TextBlockTitle.Text = apod.title;
            }

            TextBlockExplanation.Text = apod.explanation;

            if (apod.copyright != null)
            {
                TextBlockCopyright.Visibility = Visibility.Visible;
                TextBlockCopyright.Text = "Image Copyright: " + apod.copyright;
            }
            else if (apod.media_type != "video")
            {
                TextBlockCopyright.Visibility = Visibility.Collapsed;
            }
        }

        private async void ImageAstronomyPicture_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (apod.media_type == "video")
            {
                await Windows.System.Launcher.LaunchUriAsync(new Uri(apod.url));
            }
        }
    }
}
