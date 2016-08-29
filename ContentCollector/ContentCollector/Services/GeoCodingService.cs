using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ContentCollector.Services;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

[assembly: Dependency(typeof(GeoCodingService))]

namespace ContentCollector.Services
{
    public class GeoCodingService : IGeoCodingService
    {
        private readonly Geocoder _geocoder;

        public GeoCodingService()
        {
            _geocoder = new Geocoder();
        }

        public Position NullPosition
        {
            get { return new Position(0, 0); }
        }

        public async Task<Position> GeoCodeAddress(string addressString)
        {
            Position p = NullPosition;
            try
            {

                p = (await _geocoder.GetPositionsForAddressAsync(addressString)).FirstOrDefault();

            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return p;
        }

        public async Task<IEnumerable<string>> ReverseGeoCodePosition(double latitude, double longitude)
        {
            try
            {
                var position = new Position(latitude, longitude);
                var possibleAddresses = await _geocoder.GetAddressesForPositionAsync(position);
                return possibleAddresses.ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Plugin.Geolocator.Abstractions.Position> GetCurrentLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 100;
            return await locator.GetPositionAsync(timeoutMilliseconds: 10000);

        }
    }
}
