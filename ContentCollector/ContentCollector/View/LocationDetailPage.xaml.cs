using System;
using System.Linq;
using ContentCollector.Model;
using ContentCollector.Services;
using Xamarin.Forms;

namespace ContentCollector.View
{
    public partial class LocationDetailPage : ContentPage
    {
        private readonly DataManager _dataManager;
        private Location vm;

        public LocationDetailPage()
        {
            _dataManager = DataManager.DefaultManager;
            vm = new Location();
            BindingContext = vm;
            Title = "New Location";
            InitializeComponent();

        }

        public LocationDetailPage(Location location)
        {
            _dataManager = DataManager.DefaultManager;
            vm = location;
            BindingContext = vm;
            Title = vm.Name;
            InitializeComponent();
        }

        private async void OnSaveLocation(object sender, EventArgs e)
        {
            await _dataManager.SaveLocationAsync(vm);
            await Navigation.PopToRootAsync();
        }

        private async void OnGetCurrentLocation(object sender, EventArgs e)
        {
            //Get Current Location
            var geoService = new GeoCodingService();

            var results = await geoService.GetCurrentLocation();

            var latitude = results.Latitude;
            var longitude = results.Longitude;
            var altitude = results.Altitude;

            //Reverse GeoCode Results
            var reverseResults = await geoService.ReverseGeoCodePosition(results.Latitude, results.Longitude);

            var geoLoc = reverseResults.FirstOrDefault();

            vm.Geolocation = geoLoc ?? string.Empty;
            vm.Latitude = latitude;
            vm.Longitude = longitude;
            vm.Altitude = altitude;


            vm.Name = geoLoc ?? DateTime.UtcNow.ToString();


            if (vm.Id == null)
            {
                vm.CreatedTimeStamp = DateTime.UtcNow;
            }

            await _dataManager.SaveLocationAsync(vm);

            BindingContext = vm;

        }
    }
}
