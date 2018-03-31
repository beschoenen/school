using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using Database;
using GalaSoft.MvvmLight.Command;
using MindFusion.Scheduling;
using SOh_ParkInspect.Helper;
using SOh_ParkInspect.Model;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.ViewModel.Control
{
    public class EmployeeAvailabilityViewModel : ParkInspectViewModel
    {
        private readonly IAvailabilityRepository _availabilityRepository;

        public ICommand SaveCommand { get; set; }

        public ObservableCollection<Appointment> Appointments { get; }

        private readonly List<Availability> _myAvailabilities;

        public EmployeeAvailabilityViewModel(IAvailabilityRepository availabilityRepository)
        {
            _availabilityRepository = availabilityRepository;
            _myAvailabilities = new List<Availability>();

            Appointments = new ObservableCollection<Appointment>();

            SaveCommand = new RelayCommand(Save);
        }

        public void Save()
        {
            if (_myAvailabilities.Count == 0) return;

            _availabilityRepository.Save(Settings.CurrentUser, _myAvailabilities);
            MessageBox.Show("Beschikbaarheid opgeslagen!");

            Load();
        }

        public void AddAppointment(Item appointment)
        {
            _myAvailabilities.Add(new Availability
            {
                Available = true,
                Employee = Settings.CurrentUser,
                StartDateTime = appointment.StartTime,
                EndDateTime = appointment.EndTime,
                Remark = appointment.HeaderText
            });
        }

        public override void OnEnter()
        {
            Load();
        }

        private void Load()
        {
            Appointments.Clear();
            _myAvailabilities.Clear();

            _availabilityRepository.All().ForEach(x =>
            {
                if (x.EndDateTime == null) return;

                var appointment = new Appointment
                {
                    Subject = x.Remark,
                    StartTime = x.StartDateTime,
                    EndTime = x.EndDateTime.Value,
                    DescriptionText = $"{x.Employee.FirstName} {x.Employee.LastNamePrefix} {x.Employee.LastName}",
                    Locked = x.EmployeeID != Settings.CurrentUser.ID,
                    AllowMove = x.EmployeeID == Settings.CurrentUser.ID,
                    AllowChangeStart = x.EmployeeID == Settings.CurrentUser.ID,
                    AllowChangeEnd = x.EmployeeID == Settings.CurrentUser.ID
                };

                Appointments.Add(appointment);
            });

            RaisePropertyChanged(nameof(Appointments));
        }
    }
}