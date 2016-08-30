using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ContentCollector.Model;
using ContentCollector.Services;
using Xamarin.Forms;

namespace ContentCollector.View
{
    public partial class LocationListPage : ContentPage
    {
        private readonly DataManager _dataManager;
        private ObservableCollection<Location> vm;

        public LocationListPage()
        {
            _dataManager = DataManager.DefaultManager;
            Title = "Location List Page";
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await RefreshItems(syncItems: true);
        }

        private void OnLocationSelected(object sender, ItemTappedEventArgs e)
        {
            var location = e.Item as Location;
            Navigation.PushAsync(new LocationDetailPage(location));
        }

        private async Task RefreshItems(bool syncItems)
        {
            vm = await _dataManager.GetLocationsAsync(syncItems);
            BindingContext = vm;
        }
       
    }
}
