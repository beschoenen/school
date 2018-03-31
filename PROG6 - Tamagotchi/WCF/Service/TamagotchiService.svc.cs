using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using WCF.Action;
using WCF.Enum;
using WCF.GameRule;
using WCF.GameRule.Condition;
using WCF.Helper;
using WCF.Repository;

namespace WCF.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TamagotchiService : ITamagotchiService
    {
        private BaseAction CurrentAction { get; set; }
        private Tamagotchi CurrentTamagotchi { get; set; }

        private Timer UpdateTimer { get; set; }

        private List<IGameRule> _gameRules;

        private readonly ITamagotchiRepository _repo;

        public TamagotchiService(ITamagotchiRepository repo)
        {
            _repo = repo;
        }

        ////////
        // Rules

        public List<Enum.GameRule> ActiveGameRules()
        {
            var activeGameRules = new List<Enum.GameRule>();

            _gameRules.ForEach(g =>
            {
                var rule = Converter.GameRuleToEnum(g);
                if (rule.HasValue)
                {
                    activeGameRules.Add(rule.Value);
                }
            });

            return activeGameRules;
        }

        public bool ToggleRule(string rule)
        {
            if (_gameRules == null) return false;

            var ruleInstance = Converter.GameRuleToClass(rule);

            if (ruleInstance == null) return false;

            if (_gameRules.Any(r => r.GetType() == ruleInstance.GetType()))
            {
                _gameRules.Remove(_gameRules.First(r => r.GetType() == ruleInstance.GetType()));
            }
            else
            {
                _gameRules.Add(ruleInstance);
            }

            return true;
        }

        //////////////////
        // Tamagotchi CRUD

        public Tamagotchi GetTamagotchi(int id)
        {
            return new Tamagotchi(_repo.FindById(id));
        }

        public List<Tamagotchi> GetAllTamagotchi()
        {
            var allTamagotchis = new List<Tamagotchi>();

            var gameStatuses = new List<IGameRule>
            {
                new SleepDeprivation(),
                new Starvation(),
                new Munchies(),
                new Crazy()
            };

            _repo.GetAll().ForEach(t =>
            {
                var tamagotchi = new Tamagotchi(t);
                gameStatuses.ForEach(g => g.Execute(tamagotchi));

                allTamagotchis.Add(tamagotchi);
            });

            return allTamagotchis;
        }

        public Tamagotchi CreateTamagotchi(Tamagotchi tamagotchi)
        {
            var dbTamagotchi = tamagotchi.ToDbTamagotchi();

            _repo.AddOrUpdate(dbTamagotchi);
            _repo.SaveChanges();

            return new Tamagotchi(_repo.FindById(dbTamagotchi.Id));
        }

        public bool UpdateTamagotchi(int id, Tamagotchi tamagotchi)
        {
            var findTamagotchi = _repo.FindById(id);

            if (findTamagotchi == null) return false;

            findTamagotchi.Name = tamagotchi.Name;

            _repo.AddOrUpdate(findTamagotchi);
            _repo.SaveChanges();

            return true;
        }

        public bool DeleteTamagotchi(int id)
        {
            var tamagotchi = _repo.FindById(id);

            if (tamagotchi == null) return false;

            _repo.Remove(tamagotchi);
            _repo.SaveChanges();

            return true;
        }

        //////////
        // Actions

        public bool DoAction(string action)
        {
            if (CurrentAction != null) return false;

            if (CurrentTamagotchi.Statuses.Contains(Status.Crazy) && new Random(GetHashCode()).Next(1, 100) > 50)
            {
                CurrentTamagotchi.Deceased = true;
            }

            var baseAction = Converter.ActionToClass(action);

            if (baseAction == null) return false;

            CurrentAction = baseAction.Start(CurrentTamagotchi, () => CurrentAction = null);

            return true;
        }

        public KeyValuePair<string, int>? GetCurrentAction()
        {
            if (CurrentAction == null) return null;

            return new KeyValuePair<string, int>(CurrentAction.Name, CurrentAction.Duration - DateTime.Now.Subtract(CurrentAction.StartTime).Seconds);
        }

        public bool IsCurrentlyRunningAnAction()
        {
            return CurrentAction != null;
        }

        /////////////////////
        // Current Tamagotchi

        public bool SwitchTamagotchi(int id)
        {
            var tamagotchi = _repo.FindById(id);

            if (tamagotchi == null) return false;
            if (CurrentTamagotchi != null) return false;

            DismissTamagotchi();

            UpdateTimer = new Timer(10000)
            {
                AutoReset = true,
                Enabled = true
            };

            UpdateTimer.Elapsed += (sender, e) =>
            {
                _gameRules.ForEach(r => r.Execute(CurrentTamagotchi));

                if (CurrentTamagotchi.Deceased)
                {
                    Deceased();
                }

                SaveTamagotchi();
            };

            UpdateTimer.Start();

            _gameRules = new List<IGameRule>
            {
                new Age(),
                new Hunger(),
                new Sleep(),
                new Boredom(),
                new SleepDeprivation(),
                new Starvation(),
                new Munchies(),
                new Crazy()
            };

            CurrentTamagotchi = new Tamagotchi(tamagotchi);

            return true;
        }

        public bool DismissTamagotchi()
        {
            if (CurrentTamagotchi == null) return false;

            UpdateTimer?.Stop();
            UpdateTimer?.Dispose();

            CurrentAction?.Cancel();
            CurrentAction = null;

            SaveTamagotchi();

            CurrentTamagotchi = null;

            return true;
        }

        public Tamagotchi GetCurrentTamagotchi()
        {
            return CurrentTamagotchi;
        }

        private void Deceased()
        {
            UpdateTimer?.Stop();
            UpdateTimer?.Dispose();

            CurrentAction?.Cancel();
        }

        private void SaveTamagotchi()
        {
            var tamagotchi = _repo.FindById(CurrentTamagotchi.Id) ??
                             new Database.Tamagotchi();

            tamagotchi.Name = CurrentTamagotchi.Name;
            tamagotchi.Sleep = CurrentTamagotchi.Sleep;
            tamagotchi.Age = CurrentTamagotchi.Age;
            tamagotchi.Boredom = CurrentTamagotchi.Boredom;
            tamagotchi.Health = CurrentTamagotchi.Health;
            tamagotchi.Hunger = CurrentTamagotchi.Hunger;

            _repo.AddOrUpdate(tamagotchi);
            _repo.SaveChanges();
        }
    }
}