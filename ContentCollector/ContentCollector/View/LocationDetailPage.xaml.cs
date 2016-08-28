using ContentCollector.Model;
using Xamarin.Forms;

namespace ContentCollector.View
{
    public partial class LocationDetailPage : ContentPage
    {
        private Location location;

        public LocationDetailPage()
        {
            Title = "Location Detail Page";
            InitializeComponent();

        }

        public LocationDetailPage(Location location)
        {
            this.location = location;
        }
    }
}
