using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WCF.Service
{
    [ServiceContract]
    public interface ITamagotchiService
    {
        ////////
        // Rules

        [OperationContract]
        List<Enum.GameRule> ActiveGameRules();

        [OperationContract]
        bool ToggleRule(string rule);

        /////////////////////
        // Current Tamagotchi

        [OperationContract]
        Tamagotchi GetCurrentTamagotchi();

        [OperationContract]
        bool SwitchTamagotchi(int id);

        [OperationContract]
        bool DismissTamagotchi();

        /////////////////
        // Current Action

        [OperationContract]
        KeyValuePair<string, int>? GetCurrentAction();

        [OperationContract]
        bool DoAction(string action);

        [OperationContract]
        bool IsCurrentlyRunningAnAction();

        //////////////////
        // Tamagotchi CRUD

        [OperationContract]
        Tamagotchi CreateTamagotchi(Tamagotchi tamagotchi);

        [OperationContract]
        List<Tamagotchi> GetAllTamagotchi();

        [OperationContract]
        Tamagotchi GetTamagotchi(int id);

        [OperationContract]
        bool DeleteTamagotchi(int id);

        [OperationContract]
        bool UpdateTamagotchi(int id, Tamagotchi tamagotchi);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Tamagotchi
    {
        public Tamagotchi(Database.Tamagotchi tamagotchi)
        {
            Statuses = new List<string>();

            Id = tamagotchi.Id;
            Name = tamagotchi.Name;
            Age = tamagotchi.Age;
            Hunger = tamagotchi.Hunger;
            Sleep = tamagotchi.Sleep;
            Boredom = tamagotchi.Boredom;
            Health = tamagotchi.Health;
        }

        public Tamagotchi()
        {
            Statuses = new List<string>();
        }

        private int _hunger;
        private int _sleep;
        private int _boredom;
        private int _health;

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Age { get; set; }

        [DataMember]
        public int Hunger
        {
            get { return _hunger; }
            set { _hunger = Math.Max(0, Math.Min(100, value)); }
        }

        [DataMember]
        public int Sleep
        {
            get { return _sleep; }
            set { _sleep = Math.Max(0, Math.Min(100, value)); }
        }

        [DataMember]
        public int Boredom
        {
            get { return _boredom; }
            set { _boredom = Math.Max(0, Math.Min(100, value)); }
        }

        [DataMember]
        public int Health
        {
            get { return _health; }
            set { _health = Math.Max(0, Math.Min(100, value)); }
        }

        [DataMember]
        public List<string> Statuses { get; set; }

        [DataMember]
        public bool Deceased
        {
            get { return _health == 0; }
            set { _health = value ? 0 : _health; }
        }

        public Database.Tamagotchi ToDbTamagotchi()
        {
            return new Database.Tamagotchi
            {
                Id = Id,
                Name = Name,
                Age = Age,
                Hunger = Hunger,
                Sleep = Sleep,
                Boredom = Boredom,
                Health = Health
            };
        }
    }
}