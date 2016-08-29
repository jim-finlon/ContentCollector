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
            BindingContext = null;

            //Get Current Location
            var geoService = new GeoCodingService();

            var results = await geoService.GetCurrentLocation();

            vm.Latitude = results.Latitude;
            vm.Longitude = results.Longitude;
            vm.Altitude = results.Altitude;

            //Reverse GeoCode Results
            var reverseResults = await geoService.ReverseGeoCodePosition(results.Latitude, results.Longitude);

            vm.Geolocation = reverseResults.FirstOrDefault();

            if (vm.Name == null)
            {
                vm.Name = vm.Geolocation;
            }
   
            await _dataManager.SaveLocationAsync(vm);

            BindingContext = vm;

        }
    }
}
