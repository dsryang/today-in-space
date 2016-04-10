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
    public sealed partial class MarsRoverResultPage : Page
    {
        private string camera;
        private MarsRover rover;
        private List<Photo> itemsList;

        public MarsRoverResultPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                camera = e.Parameter as string;
                itemsList = new List<Photo>();
                TextBlockHeader.Text = camera + " Photos";

                await LoadImages();
            }
        }

        private async Task LoadImages()
        {
            Uri uri = new Uri("https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?api_key=" + App.nasaApiKey + "&camera=" + camera + "&earth_date=" + App.formattedDate);
            HttpClient httpClient = new HttpClient();

            HttpResponseMessage response = await httpClient.GetAsync(uri);
            string jsonString = await response.Content.ReadAsStringAsync();

            rover = JsonConvert.DeserializeObject<MarsRover>(jsonString);

            if (rover.photos != null)
            {
                TextBlockMessage.Visibility = Visibility.Collapsed;
                FlipViewGallery.Visibility = Visibility.Visible;
                FlipViewGallery.ItemsSource = rover.photos;
            }
            else
            {
                TextBlockMessage.Visibility = Visibility.Visible;
                FlipViewGallery.Visibility = Visibility.Collapsed;
                TextBlockMessage.Text = rover.errors + ". Try changing the date in settings!";
            }
        }
    }
}
