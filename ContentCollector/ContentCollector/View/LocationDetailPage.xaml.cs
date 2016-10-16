using System;
using System.Linq;
using ContentCollector.Model;
using ContentCollector.Services;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ContentCollector.View
{
    public partial class LocationDetailPage : ContentPage
    {
        private readonly DataManager _dataManager;
        private Location vm;

        private double _width = 0;
        private double _height = 0;

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
            SetMap(location.Latitude, location.Longitude);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (_width != width || _height != height)
            {
                _width = width;
                _height = height;

                if (width > height)
                {
                    OuterLayout.Orientation = StackOrientation.Horizontal;
                    InfoLayout.WidthRequest = width/2 - 30;
                    MapLayout.WidthRequest = width/2 + 30;

                    MapLayout.Padding = new Thickness(5, 5, 40, 5);
                }
                else
                {
                    OuterLayout.Orientation = StackOrientation.Vertical;

                    MapLayout.Padding = new Thickness(5, 5, 5, 5);
                }

            }

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

            if (CrossConnectivity.Current.IsConnected)
            {
                //Reverse GeoCode Results
                var reverseResults = await geoService.ReverseGeoCodePosition(results.Latitude, results.Longitude);

                vm.Geolocation = reverseResults.FirstOrDefault();

                if (vm.Name == null)
                {
                    vm.Name = vm.Geolocation;
                }
            }

            if (vm.Name == null && vm.Geolocation != null)
            {
                vm.Name = vm.Geolocation;
            }
            else if (vm.Name == null)
            {
                vm.Name = $"{vm.Latitude} / {vm.Longitude}";
            }

           // await _dataManager.SaveLocationAsync(vm);

            BindingContext = vm;

            SetMap(vm.Latitude, vm.Longitude);

        }

        private void SetMap(double latitude, double longitude)
        {
            var pin = new Pin()
            {
                Position = new Position(latitude, longitude),
                Label = vm.Name
            };

            LocationMap.Pins.Add(pin);
            LocationMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latitude, longitude), Distance.FromMiles(0.3)));
        }

        private async void OnReverseGeoCodeLocation(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                BindingContext = null;
                var geoService = new GeoCodingService();
                var reverseResults = await geoService.ReverseGeoCodePosition(vm.Latitude, vm.Longitude);

                vm.Geolocation = reverseResults.FirstOrDefault();

                if (vm.Name == $"{vm.Latitude} / {vm.Longitude}" || vm.Name == null)
                {
                    vm.Name = vm.Geolocation;
                }

                BindingContext = vm;
                // await _dataManager.SaveLocationAsync(vm);
            }
        }
    }
}
