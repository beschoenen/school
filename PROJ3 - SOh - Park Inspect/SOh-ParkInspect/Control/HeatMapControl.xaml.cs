using System.Linq;
using System.Windows;
using Database;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using SOh_ParkInspect.ViewModel.Control;

namespace SOh_ParkInspect.Control
{
    /// <summary>
    ///     Interaction logic for HeatMapControl.xaml
    /// </summary>
    public partial class HeatMapControl
    {
        private readonly HeatMapViewModel _viewModel;

        public HeatMapControl()
        {
            InitializeComponent();

            GMapControl.MapProvider = GoogleMapProvider.Instance;
            GMapControl.ShowCenter = false;

            _viewModel = (HeatMapViewModel) DataContext;

            var parkingLots = _viewModel.ParkingLots;

            foreach (var lot in parkingLots)
            {
                if (lot.GeoLatitude != null && lot.GeoLongitude != null)
                {
                    AddMarker(new PointLatLng((double) lot.GeoLatitude, (double) lot.GeoLongitude), lot);
                }
            }
            CenterMap();

            DownloadButton.Click += (sender, e) => DownloadArea();
        }

        public void AddMarker(PointLatLng pos, ParkingLot parkingLot)
        {
            var marker = new GMapMarker(pos);

            var total = _viewModel.ParkingLots.Max(e => e.Tasks.Count);

            marker.Shape = new CustomMarker(this, marker, parkingLot, total);
            marker.Offset = new Point(-12.5, -40);

            GMapControl.Markers.Add(marker);
        }

        private void CenterMap()
        {
            GMapControl.SetPositionByKeywords("Utrecht, The Netherlands");
        }

        public void SelectMarker(ParkingLot selectedLot)
        {
            _viewModel.ParkingLot = selectedLot;
        }

        private void DownloadArea()
        {
            var area = new RectLatLng(GMapControl.ViewArea.LocationMiddle, new SizeLatLng(GMapControl.ViewArea.HeightLat, GMapControl.ViewArea.WidthLng));

            if (area.IsEmpty)
            {
                return;
            }

            Enumerable.Range(GMapControl.MinZoom, GMapControl.MaxZoom).ToList().ForEach(zoom =>
            {
                var tilePrefetcher = new TilePrefetcher();
                tilePrefetcher.Start(area, zoom, GMapControl.MapProvider, 100);
            });
        }
    }
}