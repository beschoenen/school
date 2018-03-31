﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP.TamagotchiService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GameRule", Namespace="http://schemas.datacontract.org/2004/07/WCF.Enum")]
    public enum GameRule : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Age = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Boredom = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Hunger = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Sleep = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SleepDeprevation = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Starvation = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Munchies = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Crazy = 7,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Tamagotchi", Namespace="http://schemas.datacontract.org/2004/07/WCF.Service")]
    [System.SerializableAttribute()]
    public partial class Tamagotchi : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AgeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int BoredomField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool DeceasedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int HealthField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int HungerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int SleepField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<string> StatusesField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Age {
            get {
                return this.AgeField;
            }
            set {
                if ((this.AgeField.Equals(value) != true)) {
                    this.AgeField = value;
                    this.RaisePropertyChanged("Age");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Boredom {
            get {
                return this.BoredomField;
            }
            set {
                if ((this.BoredomField.Equals(value) != true)) {
                    this.BoredomField = value;
                    this.RaisePropertyChanged("Boredom");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Deceased {
            get {
                return this.DeceasedField;
            }
            set {
                if ((this.DeceasedField.Equals(value) != true)) {
                    this.DeceasedField = value;
                    this.RaisePropertyChanged("Deceased");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Health {
            get {
                return this.HealthField;
            }
            set {
                if ((this.HealthField.Equals(value) != true)) {
                    this.HealthField = value;
                    this.RaisePropertyChanged("Health");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Hunger {
            get {
                return this.HungerField;
            }
            set {
                if ((this.HungerField.Equals(value) != true)) {
                    this.HungerField = value;
                    this.RaisePropertyChanged("Hunger");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Sleep {
            get {
                return this.SleepField;
            }
            set {
                if ((this.SleepField.Equals(value) != true)) {
                    this.SleepField = value;
                    this.RaisePropertyChanged("Sleep");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<string> Statuses {
            get {
                return this.StatusesField;
            }
            set {
                if ((object.ReferenceEquals(this.StatusesField, value) != true)) {
                    this.StatusesField = value;
                    this.RaisePropertyChanged("Statuses");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TamagotchiService.ITamagotchiService")]
    public interface ITamagotchiService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/ActiveGameRules", ReplyAction="http://tempuri.org/ITamagotchiService/ActiveGameRulesResponse")]
        System.Collections.Generic.List<ASP.TamagotchiService.GameRule> ActiveGameRules();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/ActiveGameRules", ReplyAction="http://tempuri.org/ITamagotchiService/ActiveGameRulesResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<ASP.TamagotchiService.GameRule>> ActiveGameRulesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/ToggleRule", ReplyAction="http://tempuri.org/ITamagotchiService/ToggleRuleResponse")]
        bool ToggleRule(string rule);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/ToggleRule", ReplyAction="http://tempuri.org/ITamagotchiService/ToggleRuleResponse")]
        System.Threading.Tasks.Task<bool> ToggleRuleAsync(string rule);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/GetCurrentTamagotchi", ReplyAction="http://tempuri.org/ITamagotchiService/GetCurrentTamagotchiResponse")]
        ASP.TamagotchiService.Tamagotchi GetCurrentTamagotchi();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/GetCurrentTamagotchi", ReplyAction="http://tempuri.org/ITamagotchiService/GetCurrentTamagotchiResponse")]
        System.Threading.Tasks.Task<ASP.TamagotchiService.Tamagotchi> GetCurrentTamagotchiAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/SwitchTamagotchi", ReplyAction="http://tempuri.org/ITamagotchiService/SwitchTamagotchiResponse")]
        bool SwitchTamagotchi(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/SwitchTamagotchi", ReplyAction="http://tempuri.org/ITamagotchiService/SwitchTamagotchiResponse")]
        System.Threading.Tasks.Task<bool> SwitchTamagotchiAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/DismissTamagotchi", ReplyAction="http://tempuri.org/ITamagotchiService/DismissTamagotchiResponse")]
        bool DismissTamagotchi();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/DismissTamagotchi", ReplyAction="http://tempuri.org/ITamagotchiService/DismissTamagotchiResponse")]
        System.Threading.Tasks.Task<bool> DismissTamagotchiAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/GetCurrentAction", ReplyAction="http://tempuri.org/ITamagotchiService/GetCurrentActionResponse")]
        System.Nullable<System.Collections.Generic.KeyValuePair<string, int>> GetCurrentAction();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/GetCurrentAction", ReplyAction="http://tempuri.org/ITamagotchiService/GetCurrentActionResponse")]
        System.Threading.Tasks.Task<System.Nullable<System.Collections.Generic.KeyValuePair<string, int>>> GetCurrentActionAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/DoAction", ReplyAction="http://tempuri.org/ITamagotchiService/DoActionResponse")]
        bool DoAction(string action);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/DoAction", ReplyAction="http://tempuri.org/ITamagotchiService/DoActionResponse")]
        System.Threading.Tasks.Task<bool> DoActionAsync(string action);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/IsCurrentlyRunningAnAction", ReplyAction="http://tempuri.org/ITamagotchiService/IsCurrentlyRunningAnActionResponse")]
        bool IsCurrentlyRunningAnAction();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/IsCurrentlyRunningAnAction", ReplyAction="http://tempuri.org/ITamagotchiService/IsCurrentlyRunningAnActionResponse")]
        System.Threading.Tasks.Task<bool> IsCurrentlyRunningAnActionAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/CreateTamagotchi", ReplyAction="http://tempuri.org/ITamagotchiService/CreateTamagotchiResponse")]
        ASP.TamagotchiService.Tamagotchi CreateTamagotchi(ASP.TamagotchiService.Tamagotchi tamagotchi);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/CreateTamagotchi", ReplyAction="http://tempuri.org/ITamagotchiService/CreateTamagotchiResponse")]
        System.Threading.Tasks.Task<ASP.TamagotchiService.Tamagotchi> CreateTamagotchiAsync(ASP.TamagotchiService.Tamagotchi tamagotchi);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/GetAllTamagotchi", ReplyAction="http://tempuri.org/ITamagotchiService/GetAllTamagotchiResponse")]
        System.Collections.Generic.List<ASP.TamagotchiService.Tamagotchi> GetAllTamagotchi();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/GetAllTamagotchi", ReplyAction="http://tempuri.org/ITamagotchiService/GetAllTamagotchiResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<ASP.TamagotchiService.Tamagotchi>> GetAllTamagotchiAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/GetTamagotchi", ReplyAction="http://tempuri.org/ITamagotchiService/GetTamagotchiResponse")]
        ASP.TamagotchiService.Tamagotchi GetTamagotchi(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/GetTamagotchi", ReplyAction="http://tempuri.org/ITamagotchiService/GetTamagotchiResponse")]
        System.Threading.Tasks.Task<ASP.TamagotchiService.Tamagotchi> GetTamagotchiAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/DeleteTamagotchi", ReplyAction="http://tempuri.org/ITamagotchiService/DeleteTamagotchiResponse")]
        bool DeleteTamagotchi(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/DeleteTamagotchi", ReplyAction="http://tempuri.org/ITamagotchiService/DeleteTamagotchiResponse")]
        System.Threading.Tasks.Task<bool> DeleteTamagotchiAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/UpdateTamagotchi", ReplyAction="http://tempuri.org/ITamagotchiService/UpdateTamagotchiResponse")]
        bool UpdateTamagotchi(int id, ASP.TamagotchiService.Tamagotchi tamagotchi);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITamagotchiService/UpdateTamagotchi", ReplyAction="http://tempuri.org/ITamagotchiService/UpdateTamagotchiResponse")]
        System.Threading.Tasks.Task<bool> UpdateTamagotchiAsync(int id, ASP.TamagotchiService.Tamagotchi tamagotchi);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITamagotchiServiceChannel : ASP.TamagotchiService.ITamagotchiService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TamagotchiServiceClient : System.ServiceModel.ClientBase<ASP.TamagotchiService.ITamagotchiService>, ASP.TamagotchiService.ITamagotchiService {
        
        public TamagotchiServiceClient() {
        }
        
        public TamagotchiServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TamagotchiServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TamagotchiServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TamagotchiServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<ASP.TamagotchiService.GameRule> ActiveGameRules() {
            return base.Channel.ActiveGameRules();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<ASP.TamagotchiService.GameRule>> ActiveGameRulesAsync() {
            return base.Channel.ActiveGameRulesAsync();
        }
        
        public bool ToggleRule(string rule) {
            return base.Channel.ToggleRule(rule);
        }
        
        public System.Threading.Tasks.Task<bool> ToggleRuleAsync(string rule) {
            return base.Channel.ToggleRuleAsync(rule);
        }
        
        public ASP.TamagotchiService.Tamagotchi GetCurrentTamagotchi() {
            return base.Channel.GetCurrentTamagotchi();
        }
        
        public System.Threading.Tasks.Task<ASP.TamagotchiService.Tamagotchi> GetCurrentTamagotchiAsync() {
            return base.Channel.GetCurrentTamagotchiAsync();
        }
        
        public bool SwitchTamagotchi(int id) {
            return base.Channel.SwitchTamagotchi(id);
        }
        
        public System.Threading.Tasks.Task<bool> SwitchTamagotchiAsync(int id) {
            return base.Channel.SwitchTamagotchiAsync(id);
        }
        
        public bool DismissTamagotchi() {
            return base.Channel.DismissTamagotchi();
        }
        
        public System.Threading.Tasks.Task<bool> DismissTamagotchiAsync() {
            return base.Channel.DismissTamagotchiAsync();
        }
        
        public System.Nullable<System.Collections.Generic.KeyValuePair<string, int>> GetCurrentAction() {
            return base.Channel.GetCurrentAction();
        }
        
        public System.Threading.Tasks.Task<System.Nullable<System.Collections.Generic.KeyValuePair<string, int>>> GetCurrentActionAsync() {
            return base.Channel.GetCurrentActionAsync();
        }
        
        public bool DoAction(string action) {
            return base.Channel.DoAction(action);
        }
        
        public System.Threading.Tasks.Task<bool> DoActionAsync(string action) {
            return base.Channel.DoActionAsync(action);
        }
        
        public bool IsCurrentlyRunningAnAction() {
            return base.Channel.IsCurrentlyRunningAnAction();
        }
        
        public System.Threading.Tasks.Task<bool> IsCurrentlyRunningAnActionAsync() {
            return base.Channel.IsCurrentlyRunningAnActionAsync();
        }
        
        public ASP.TamagotchiService.Tamagotchi CreateTamagotchi(ASP.TamagotchiService.Tamagotchi tamagotchi) {
            return base.Channel.CreateTamagotchi(tamagotchi);
        }
        
        public System.Threading.Tasks.Task<ASP.TamagotchiService.Tamagotchi> CreateTamagotchiAsync(ASP.TamagotchiService.Tamagotchi tamagotchi) {
            return base.Channel.CreateTamagotchiAsync(tamagotchi);
        }
        
        public System.Collections.Generic.List<ASP.TamagotchiService.Tamagotchi> GetAllTamagotchi() {
            return base.Channel.GetAllTamagotchi();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<ASP.TamagotchiService.Tamagotchi>> GetAllTamagotchiAsync() {
            return base.Channel.GetAllTamagotchiAsync();
        }
        
        public ASP.TamagotchiService.Tamagotchi GetTamagotchi(int id) {
            return base.Channel.GetTamagotchi(id);
        }
        
        public System.Threading.Tasks.Task<ASP.TamagotchiService.Tamagotchi> GetTamagotchiAsync(int id) {
            return base.Channel.GetTamagotchiAsync(id);
        }
        
        public bool DeleteTamagotchi(int id) {
            return base.Channel.DeleteTamagotchi(id);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteTamagotchiAsync(int id) {
            return base.Channel.DeleteTamagotchiAsync(id);
        }
        
        public bool UpdateTamagotchi(int id, ASP.TamagotchiService.Tamagotchi tamagotchi) {
            return base.Channel.UpdateTamagotchi(id, tamagotchi);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateTamagotchiAsync(int id, ASP.TamagotchiService.Tamagotchi tamagotchi) {
            return base.Channel.UpdateTamagotchiAsync(id, tamagotchi);
        }
    }
}
