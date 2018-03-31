using System;
using System.Windows;
using System.Windows.Input;
using Database;
using GalaSoft.MvvmLight.Command;
using GoogleMaps.LocationServices;
using SOh_ParkInspect.Helper;
using SOh_ParkInspect.Repository.Interface;
using static CryptSharp.Crypter;

namespace SOh_ParkInspect.ViewModel.Control
{
    public class LocationInfoViewModel : ParkInspectViewModel
    {
        private readonly RouterService _router;
        private readonly IParkingLotRepository _repository;

        public ICommand ReturnCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        private ParkingLot _parkingLot;

        public ParkingLot ParkingLot
        {
            get { return _parkingLot; }
            set
            {
                _parkingLot = value;
                RaisePropertyChanged();
            }
        }

        public LocationInfoViewModel(RouterService routerService, IParkingLotRepository repository)
        {
            _repository = repository;
            _router = routerService;

            ReturnCommand = new RelayCommand(_router.GoBack);
            SaveCommand = new RelayCommand(Save);
        }

        private void Save()
        {
            var point = GetLatLong();

            var error = CanSave(point);
            if (error != string.Empty)
            {
                MessageBox.Show(error);
                return;
            }

            ParkingLot.GeoLongitude = point.Longitude;
            ParkingLot.GeoLatitude = point.Latitude;

            _repository.Add(ParkingLot);

            MessageBox.Show("De locatie is opgeslagen!");
            _router.GoBack();
        }

        private MapPoint GetLatLong()
        {
            var locationService = new GoogleLocationService();

            return locationService.GetLatLongFromAddress(
                $"{ParkingLot.Address.Street} {ParkingLot.Address.Number} {ParkingLot.Address.City} {ParkingLot.Address.Country}"
            );
        }

        private string CanSave(MapPoint point)
        {
            if (
                string.IsNullOrWhiteSpace(ParkingLot.Address.Street) ||
                string.IsNullOrWhiteSpace(ParkingLot.Address.Number) ||
                string.IsNullOrWhiteSpace(ParkingLot.Address.ZipCode) ||
                string.IsNullOrWhiteSpace(ParkingLot.Address.City) ||
                string.IsNullOrWhiteSpace(ParkingLot.Address.Province) ||
                string.IsNullOrWhiteSpace(ParkingLot.Address.Country)
            )
            {
                return "Niet alle velden zijn ingevuld!";
            }

            if (point == null)
            {
                return "Ongeldige locatie!";
            }

            return string.Empty;
        }

        public override void OnEnter()
        {
            ParkingLot = ViewBag?.ParkingLot == null ? new ParkingLot
            {
                Hash = MD5.Crypt($"{DateTime.Now}-ParkingLot"),
                Address = new Address
                {
                    Hash = MD5.Crypt($"Address-{DateTime.Now}")
                }
            } : ViewBag.ParkingLot;
        }
    }
}