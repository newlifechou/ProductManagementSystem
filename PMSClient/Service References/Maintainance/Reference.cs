﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PMSClient.Maintainance {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DcMaintenancePlan", Namespace="http://schemas.datacontract.org/2004/07/PMSWCFService.DataContracts")]
    [System.SerializableAttribute()]
    public partial class DcMaintenancePlan : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CommonFailureField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ContentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreateTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CreatorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PlanIntervalField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PlanItemField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PlanTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ProcessMethodField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RemarkField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StandardField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string VHPMachineCodeField;
        
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
        public string CommonFailure {
            get {
                return this.CommonFailureField;
            }
            set {
                if ((object.ReferenceEquals(this.CommonFailureField, value) != true)) {
                    this.CommonFailureField = value;
                    this.RaisePropertyChanged("CommonFailure");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Content {
            get {
                return this.ContentField;
            }
            set {
                if ((object.ReferenceEquals(this.ContentField, value) != true)) {
                    this.ContentField = value;
                    this.RaisePropertyChanged("Content");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CreateTime {
            get {
                return this.CreateTimeField;
            }
            set {
                if ((this.CreateTimeField.Equals(value) != true)) {
                    this.CreateTimeField = value;
                    this.RaisePropertyChanged("CreateTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Creator {
            get {
                return this.CreatorField;
            }
            set {
                if ((object.ReferenceEquals(this.CreatorField, value) != true)) {
                    this.CreatorField = value;
                    this.RaisePropertyChanged("Creator");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PlanInterval {
            get {
                return this.PlanIntervalField;
            }
            set {
                if ((object.ReferenceEquals(this.PlanIntervalField, value) != true)) {
                    this.PlanIntervalField = value;
                    this.RaisePropertyChanged("PlanInterval");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PlanItem {
            get {
                return this.PlanItemField;
            }
            set {
                if ((object.ReferenceEquals(this.PlanItemField, value) != true)) {
                    this.PlanItemField = value;
                    this.RaisePropertyChanged("PlanItem");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PlanType {
            get {
                return this.PlanTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.PlanTypeField, value) != true)) {
                    this.PlanTypeField = value;
                    this.RaisePropertyChanged("PlanType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ProcessMethod {
            get {
                return this.ProcessMethodField;
            }
            set {
                if ((object.ReferenceEquals(this.ProcessMethodField, value) != true)) {
                    this.ProcessMethodField = value;
                    this.RaisePropertyChanged("ProcessMethod");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Remark {
            get {
                return this.RemarkField;
            }
            set {
                if ((object.ReferenceEquals(this.RemarkField, value) != true)) {
                    this.RemarkField = value;
                    this.RaisePropertyChanged("Remark");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Standard {
            get {
                return this.StandardField;
            }
            set {
                if ((object.ReferenceEquals(this.StandardField, value) != true)) {
                    this.StandardField = value;
                    this.RaisePropertyChanged("Standard");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string State {
            get {
                return this.StateField;
            }
            set {
                if ((object.ReferenceEquals(this.StateField, value) != true)) {
                    this.StateField = value;
                    this.RaisePropertyChanged("State");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string VHPMachineCode {
            get {
                return this.VHPMachineCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.VHPMachineCodeField, value) != true)) {
                    this.VHPMachineCodeField = value;
                    this.RaisePropertyChanged("VHPMachineCode");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DcMaintenanceRecord", Namespace="http://schemas.datacontract.org/2004/07/PMSWCFService.DataContracts")]
    [System.SerializableAttribute()]
    public partial class DcMaintenanceRecord : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ContentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreateTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CreatorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LogField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PersonsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PlanIntervalField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PlanItemField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PlanTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RemarkField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string VHPMachineCodeField;
        
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
        public string Content {
            get {
                return this.ContentField;
            }
            set {
                if ((object.ReferenceEquals(this.ContentField, value) != true)) {
                    this.ContentField = value;
                    this.RaisePropertyChanged("Content");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CreateTime {
            get {
                return this.CreateTimeField;
            }
            set {
                if ((this.CreateTimeField.Equals(value) != true)) {
                    this.CreateTimeField = value;
                    this.RaisePropertyChanged("CreateTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Creator {
            get {
                return this.CreatorField;
            }
            set {
                if ((object.ReferenceEquals(this.CreatorField, value) != true)) {
                    this.CreatorField = value;
                    this.RaisePropertyChanged("Creator");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Log {
            get {
                return this.LogField;
            }
            set {
                if ((object.ReferenceEquals(this.LogField, value) != true)) {
                    this.LogField = value;
                    this.RaisePropertyChanged("Log");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Persons {
            get {
                return this.PersonsField;
            }
            set {
                if ((object.ReferenceEquals(this.PersonsField, value) != true)) {
                    this.PersonsField = value;
                    this.RaisePropertyChanged("Persons");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PlanInterval {
            get {
                return this.PlanIntervalField;
            }
            set {
                if ((object.ReferenceEquals(this.PlanIntervalField, value) != true)) {
                    this.PlanIntervalField = value;
                    this.RaisePropertyChanged("PlanInterval");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PlanItem {
            get {
                return this.PlanItemField;
            }
            set {
                if ((object.ReferenceEquals(this.PlanItemField, value) != true)) {
                    this.PlanItemField = value;
                    this.RaisePropertyChanged("PlanItem");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PlanType {
            get {
                return this.PlanTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.PlanTypeField, value) != true)) {
                    this.PlanTypeField = value;
                    this.RaisePropertyChanged("PlanType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Remark {
            get {
                return this.RemarkField;
            }
            set {
                if ((object.ReferenceEquals(this.RemarkField, value) != true)) {
                    this.RemarkField = value;
                    this.RaisePropertyChanged("Remark");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string State {
            get {
                return this.StateField;
            }
            set {
                if ((object.ReferenceEquals(this.StateField, value) != true)) {
                    this.StateField = value;
                    this.RaisePropertyChanged("State");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string VHPMachineCode {
            get {
                return this.VHPMachineCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.VHPMachineCodeField, value) != true)) {
                    this.VHPMachineCodeField = value;
                    this.RaisePropertyChanged("VHPMachineCode");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Maintainance.IMaintenanceService")]
    public interface IMaintenanceService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMaintenanceService/GetMaintenancePlans", ReplyAction="http://tempuri.org/IMaintenanceService/GetMaintenancePlansResponse")]
        PMSClient.Maintainance.DcMaintenancePlan[] GetMaintenancePlans(int s, int t, string devicecode, string planitem);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMaintenanceService/GetMaintenancePlans", ReplyAction="http://tempuri.org/IMaintenanceService/GetMaintenancePlansResponse")]
        System.Threading.Tasks.Task<PMSClient.Maintainance.DcMaintenancePlan[]> GetMaintenancePlansAsync(int s, int t, string devicecode, string planitem);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMaintenanceService/GetMaintenancePlanCount", ReplyAction="http://tempuri.org/IMaintenanceService/GetMaintenancePlanCountResponse")]
        int GetMaintenancePlanCount(string devicecode, string planitem);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMaintenanceService/GetMaintenancePlanCount", ReplyAction="http://tempuri.org/IMaintenanceService/GetMaintenancePlanCountResponse")]
        System.Threading.Tasks.Task<int> GetMaintenancePlanCountAsync(string devicecode, string planitem);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMaintenanceService/AddMainitenancePlan", ReplyAction="http://tempuri.org/IMaintenanceService/AddMainitenancePlanResponse")]
        int AddMainitenancePlan(PMSClient.Maintainance.DcMaintenancePlan model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMaintenanceService/AddMainitenancePlan", ReplyAction="http://tempuri.org/IMaintenanceService/AddMainitenancePlanResponse")]
        System.Threading.Tasks.Task<int> AddMainitenancePlanAsync(PMSClient.Maintainance.DcMaintenancePlan model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMaintenanceService/UpdateMainitenancePlan", ReplyAction="http://tempuri.org/IMaintenanceService/UpdateMainitenancePlanResponse")]
        int UpdateMainitenancePlan(PMSClient.Maintainance.DcMaintenancePlan model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMaintenanceService/UpdateMainitenancePlan", ReplyAction="http://tempuri.org/IMaintenanceService/UpdateMainitenancePlanResponse")]
        System.Threading.Tasks.Task<int> UpdateMainitenancePlanAsync(PMSClient.Maintainance.DcMaintenancePlan model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMaintenanceService/GetMaintenanceRecords", ReplyAction="http://tempuri.org/IMaintenanceService/GetMaintenanceRecordsResponse")]
        PMSClient.Maintainance.DcMaintenanceRecord[] GetMaintenanceRecords(int s, int t, string device, string planitem);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMaintenanceService/GetMaintenanceRecords", ReplyAction="http://tempuri.org/IMaintenanceService/GetMaintenanceRecordsResponse")]
        System.Threading.Tasks.Task<PMSClient.Maintainance.DcMaintenanceRecord[]> GetMaintenanceRecordsAsync(int s, int t, string device, string planitem);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMaintenanceService/GetMaintenanceRecordsCount", ReplyAction="http://tempuri.org/IMaintenanceService/GetMaintenanceRecordsCountResponse")]
        int GetMaintenanceRecordsCount(string device, string planitem);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMaintenanceService/GetMaintenanceRecordsCount", ReplyAction="http://tempuri.org/IMaintenanceService/GetMaintenanceRecordsCountResponse")]
        System.Threading.Tasks.Task<int> GetMaintenanceRecordsCountAsync(string device, string planitem);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMaintenanceService/AddMainitenanceRecord", ReplyAction="http://tempuri.org/IMaintenanceService/AddMainitenanceRecordResponse")]
        int AddMainitenanceRecord(PMSClient.Maintainance.DcMaintenanceRecord model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMaintenanceService/AddMainitenanceRecord", ReplyAction="http://tempuri.org/IMaintenanceService/AddMainitenanceRecordResponse")]
        System.Threading.Tasks.Task<int> AddMainitenanceRecordAsync(PMSClient.Maintainance.DcMaintenanceRecord model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMaintenanceService/UpdateMainitenanceRecord", ReplyAction="http://tempuri.org/IMaintenanceService/UpdateMainitenanceRecordResponse")]
        int UpdateMainitenanceRecord(PMSClient.Maintainance.DcMaintenanceRecord model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMaintenanceService/UpdateMainitenanceRecord", ReplyAction="http://tempuri.org/IMaintenanceService/UpdateMainitenanceRecordResponse")]
        System.Threading.Tasks.Task<int> UpdateMainitenanceRecordAsync(PMSClient.Maintainance.DcMaintenanceRecord model);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMaintenanceServiceChannel : PMSClient.Maintainance.IMaintenanceService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MaintenanceServiceClient : System.ServiceModel.ClientBase<PMSClient.Maintainance.IMaintenanceService>, PMSClient.Maintainance.IMaintenanceService {
        
        public MaintenanceServiceClient() {
        }
        
        public MaintenanceServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MaintenanceServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MaintenanceServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MaintenanceServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public PMSClient.Maintainance.DcMaintenancePlan[] GetMaintenancePlans(int s, int t, string devicecode, string planitem) {
            return base.Channel.GetMaintenancePlans(s, t, devicecode, planitem);
        }
        
        public System.Threading.Tasks.Task<PMSClient.Maintainance.DcMaintenancePlan[]> GetMaintenancePlansAsync(int s, int t, string devicecode, string planitem) {
            return base.Channel.GetMaintenancePlansAsync(s, t, devicecode, planitem);
        }
        
        public int GetMaintenancePlanCount(string devicecode, string planitem) {
            return base.Channel.GetMaintenancePlanCount(devicecode, planitem);
        }
        
        public System.Threading.Tasks.Task<int> GetMaintenancePlanCountAsync(string devicecode, string planitem) {
            return base.Channel.GetMaintenancePlanCountAsync(devicecode, planitem);
        }
        
        public int AddMainitenancePlan(PMSClient.Maintainance.DcMaintenancePlan model) {
            return base.Channel.AddMainitenancePlan(model);
        }
        
        public System.Threading.Tasks.Task<int> AddMainitenancePlanAsync(PMSClient.Maintainance.DcMaintenancePlan model) {
            return base.Channel.AddMainitenancePlanAsync(model);
        }
        
        public int UpdateMainitenancePlan(PMSClient.Maintainance.DcMaintenancePlan model) {
            return base.Channel.UpdateMainitenancePlan(model);
        }
        
        public System.Threading.Tasks.Task<int> UpdateMainitenancePlanAsync(PMSClient.Maintainance.DcMaintenancePlan model) {
            return base.Channel.UpdateMainitenancePlanAsync(model);
        }
        
        public PMSClient.Maintainance.DcMaintenanceRecord[] GetMaintenanceRecords(int s, int t, string device, string planitem) {
            return base.Channel.GetMaintenanceRecords(s, t, device, planitem);
        }
        
        public System.Threading.Tasks.Task<PMSClient.Maintainance.DcMaintenanceRecord[]> GetMaintenanceRecordsAsync(int s, int t, string device, string planitem) {
            return base.Channel.GetMaintenanceRecordsAsync(s, t, device, planitem);
        }
        
        public int GetMaintenanceRecordsCount(string device, string planitem) {
            return base.Channel.GetMaintenanceRecordsCount(device, planitem);
        }
        
        public System.Threading.Tasks.Task<int> GetMaintenanceRecordsCountAsync(string device, string planitem) {
            return base.Channel.GetMaintenanceRecordsCountAsync(device, planitem);
        }
        
        public int AddMainitenanceRecord(PMSClient.Maintainance.DcMaintenanceRecord model) {
            return base.Channel.AddMainitenanceRecord(model);
        }
        
        public System.Threading.Tasks.Task<int> AddMainitenanceRecordAsync(PMSClient.Maintainance.DcMaintenanceRecord model) {
            return base.Channel.AddMainitenanceRecordAsync(model);
        }
        
        public int UpdateMainitenanceRecord(PMSClient.Maintainance.DcMaintenanceRecord model) {
            return base.Channel.UpdateMainitenanceRecord(model);
        }
        
        public System.Threading.Tasks.Task<int> UpdateMainitenanceRecordAsync(PMSClient.Maintainance.DcMaintenanceRecord model) {
            return base.Channel.UpdateMainitenanceRecordAsync(model);
        }
    }
}
