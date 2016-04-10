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
using Windows.UI.Xaml.Navigation;

namespace TodayInSpace
{
    public sealed partial class LocationImageryPage : Page
    {
        Geopoint mapLocationPoint;

        public LocationImageryPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                (e.Parameter as RadioButton).IsChecked = true;
            }

            MapControlLocation.MapServiceToken = App.bingMapsApiKey;
            ShowDefaultLocation();
        }

        private async void ShowDefaultLocation()
        {
            // Defaults to the University of Waterloo
            BasicGeoposition position = new BasicGeoposition();
            position.Latitude = 43.4722854;
            position.Longitude = -80.5448576;
            mapLocationPoint = new Geopoint(position);

            await MapControlLocation.TrySetViewAsync(mapLocationPoint, 15D);
        }

        private async void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                if (sender.Text.Length > 2)
                {
                    sender.ItemsSource = await getMapSuggestionsAsync(sender.Text, mapLocationPoint);
                }
                else
                {
                    sender.ItemsSource = new List<MapLocation> { };
                }
            }
        }

        public static async Task<List<MapLocation>> getMapSuggestionsAsync(String query, Geopoint hintPoint)
        {
            List<MapLocation> locations = new List<MapLocation>();
            // Find a corresponding location
            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync(query, hintPoint, 10);
            
            if (result.Status != MapLocationFinderStatus.Success)
            {
                return locations;
            }

            foreach (var location in result.Locations)
            {
                locations.Add(location);
            }

            return locations;
        }

        private async void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            MapLocation selected = args.SelectedItem as MapLocation;
            sender.Text = selected.Address.FormattedAddress;
            mapLocationPoint = selected.Point;

            await MapControlLocation.TrySetViewAsync(mapLocationPoint, 15D);
        }

        private void ButtonFindImagery_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LocationImageryResultPage), mapLocationPoint);
        }
    }
}
