using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentCollector.View;
using Xamarin.Forms;

namespace ContentCollector
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Title = "Main Page";
            InitializeComponent();
        }

        private void OnListLocations(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LocationListPage());
        }

        private void OnAddLocation(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LocationDetailPage());
        }
    }
}
