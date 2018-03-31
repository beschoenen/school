using System.Windows;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using GoogleMaps.LocationServices;

namespace SOh_ParkInspect.Control
{
    public partial class LocationInfoControl
    {
        public LocationInfoControl()
        {
            InitializeComponent();

            GMapControl.MapProvider = GoogleMapProvider.Instance;
            GMapControl.ShowCenter = false;
        }

        private void CheckLocationClick(object sender, RoutedEventArgs e)
        {
            GMapControl.Markers.Clear();

            var addresString = $"{Street.Text} {Number.Text} {City.Text} {Country.Text}";

            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(addresString);

            if (point == null) return;

            var marker = new GMapMarker(new PointLatLng(point.Latitude, point.Longitude))
            {
                Shape = new CustomMarker(),
                Offset = new Point(-12.5, -40)
            };

            GMapControl.Markers.Add(marker);
            GMapControl.SetPositionByKeywords($"{City.Text}, {Country.Text}");
        }
    }
}