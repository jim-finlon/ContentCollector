using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace ContentCollector.Services
{
    public interface IGeoCodingService
    {
        Position NullPosition { get; }

        Task<Position> GeoCodeAddress(string addressString);

        Task<IEnumerable<string>> ReverseGeoCodePosition(double latitude, double longitude);

        Task<Plugin.Geolocator.Abstractions.Position> GetCurrentLocation();
    }
}
