using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Database;
using GMap.NET.WindowsPresentation;

namespace SOh_ParkInspect.Control
{
    public partial class CustomMarker
    {
        private readonly HeatMapControl _mainWindow;
        private readonly GMapMarker _marker;
        private readonly ParkingLot _parkingLot;
        private readonly Popup _popup;

        public CustomMarker(HeatMapControl window, GMapMarker marker, ParkingLot parkingLot, int total)
        {
            InitializeComponent();

            _mainWindow = window;
            _marker = marker;

            var tasksOnLot = parkingLot.Tasks.Sum(t => t.Inspections.Count);
 
            if (tasksOnLot < total / 2)
            {
                Icon.Source = new BitmapImage(new Uri("/Asset/GMapsMarkerLow.png", UriKind.Relative));
            }
            else if (tasksOnLot < total)
            {
                Icon.Source = new BitmapImage(new Uri("/Asset/GMapsMarkerMedium.png", UriKind.Relative));
            }
            else
            {
                Icon.Source = new BitmapImage(new Uri("/Asset/GMapsMarkerHigh.png", UriKind.Relative));
            }

            _parkingLot = parkingLot;

            _popup = new Popup();
            var label = new Label();

            MouseEnter += MarkerControl_MouseEnter;
            MouseLeave += MarkerControl_MouseLeave;
            MouseLeftButtonUp += CustomMarkerDemo_MouseLeftButtonUp;

            _popup.Placement = PlacementMode.Mouse;

            label.Background = Brushes.Blue;
            label.Foreground = Brushes.White;
            label.BorderBrush = Brushes.WhiteSmoke;
            label.BorderThickness = new Thickness(2);
            label.Padding = new Thickness(5);
            label.FontSize = 20;
            label.Content = parkingLot.Address.Street;

            _popup.Child = label;
        }

        public CustomMarker()
        {
            InitializeComponent();

            Icon.Source = new BitmapImage(new Uri("/Asset/GMapsMarkerLow.png", UriKind.Relative));
        }

        private void CustomMarkerDemo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _mainWindow.SelectMarker(_parkingLot);
        }

        private void MarkerControl_MouseLeave(object sender, MouseEventArgs e)
        {
            _marker.ZIndex -= 10000;
            _popup.IsOpen = false;
        }

        private void MarkerControl_MouseEnter(object sender, MouseEventArgs e)
        {
            _marker.ZIndex += 10000;
            _popup.IsOpen = true;
        }
    }
}