using ContentCollector.Model;
using Xamarin.Forms;

namespace ContentCollector.View
{
    public partial class LocationListPage : ContentPage
    {
        public LocationListPage()
        {
            Title = "Location List Page";
            InitializeComponent();

        }

        private void OnLocationSelected(object sender, ItemTappedEventArgs e)
        {
            var location = e.Item as Location;
            Navigation.PushAsync(new LocationDetailPage(location));
        }
    }
}
