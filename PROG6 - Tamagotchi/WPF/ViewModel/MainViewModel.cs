using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using WPF.TamagotchiService;

namespace WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ITamagotchiService _service;

        private Tamagotchi _tamagotchi;

        public Tamagotchi Tamagotchi
        {
            get { return _tamagotchi; }
            private set
            {
                _tamagotchi = value;
                RaisePropertyChanged();
            }
        }

        private KeyValuePair<string, int> _currentAction;

        public KeyValuePair<string, int> CurrentAction
        {
            get { return _currentAction; }
            private set
            {
                _currentAction = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(ActionString));
            }
        }

        public string ActionString => CurrentAction.Key == string.Empty ? "" : $"{CurrentAction.Key} ({CurrentAction.Value})";

        private string _currentStatus;

        public string CurrentStatus
        {
            get { return _currentStatus; }
            private set
            {
                _currentStatus = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ListItemViewModel> TamagotchiList { get; set; }

        public MainViewModel(ITamagotchiService service)
        {
            _service = service;

            TamagotchiList = new ObservableCollection<ListItemViewModel>();

            Update();

            var timer = new Timer(2000)
            {
                AutoReset = true,
                Enabled = true
            };
            timer.Elapsed += (sender, e) => Update();
        }

        private void Update()
        {
            Tamagotchi = _service.GetCurrentTamagotchi();

            var currentAction = _service.GetCurrentAction();
            CurrentAction = currentAction == null ? new KeyValuePair<string, int>(string.Empty, 0) : currentAction.Value; 

            var dispatcher = App.Current?.Dispatcher ?? Dispatcher.CurrentDispatcher;

            dispatcher.Invoke(delegate { FillTamagotchiList(); });

            CurrentStatus = UpdateCurrentStatus(Tamagotchi);
        }

        public void FillTamagotchiList()
        {
            TamagotchiList.Clear();

            _service.GetAllTamagotchi()
                .OrderBy(t => t.Deceased).ToList()
                .ForEach(t => TamagotchiList.Add(new ListItemViewModel {Tamagotchi = t}));
        }

        public string UpdateCurrentStatus(Tamagotchi tamagotchi)
        {
            if (tamagotchi?.Statuses?.Count > 0)
            {
                return $"{tamagotchi.Name} is currently:\r\n{string.Join("\r\n", tamagotchi.Statuses)}";
            }
            return string.Empty;
        }
    }
}