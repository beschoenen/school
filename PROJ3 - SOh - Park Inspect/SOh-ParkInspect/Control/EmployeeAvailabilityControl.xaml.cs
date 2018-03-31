using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Database;
using MindFusion.Scheduling;
using SOh_ParkInspect.Model;
using SOh_ParkInspect.ViewModel.Control;

namespace SOh_ParkInspect.Control
{
    public partial class EmployeeAvailabilityControl
    {
        private readonly EmployeeAvailabilityViewModel _viewModel;

        public EmployeeAvailabilityControl()
        {
            InitializeComponent();

            _viewModel = (EmployeeAvailabilityViewModel) DataContext;

            _viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "Appointments")
                {
                    LoadCalendar();
                }
            };

            Schedule.Schedule = Calendar.Schedule;

            Calendar.Selection.Set(DateTime.Today);

            Calendar.Schedule.Items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null) return;
            foreach (Appointment eNewItem in e.NewItems)
            {
                eNewItem.DescriptionText = $"{Settings.CurrentUser.FirstName} {Settings.CurrentUser.LastNamePrefix} {Settings.CurrentUser.LastName}";
            }
        }

        private void LoadCalendar()
        {
            _viewModel.Appointments.ToList().ForEach(Calendar.Schedule.Items.Add);
        }

        private void Selection_OnChanged(object sender, EventArgs e)
        {
            if (Calendar.Selection.IsEmpty)
            {
                Schedule.TimetableSettings.Dates.Clear();
                return;
            }

            Schedule.BeginInit();
            Schedule.TimetableSettings.Dates.Clear();
            Schedule.TimetableSettings.Dates.Add(Calendar.Selection.Ranges[0]);
            Schedule.EndInit();
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            Calendar.Schedule.Items.Where(x => !x.Locked).ToList().ForEach(_viewModel.AddAppointment);
            Calendar.Schedule.Items.Clear();
            _viewModel.Save();
        }
    }
}