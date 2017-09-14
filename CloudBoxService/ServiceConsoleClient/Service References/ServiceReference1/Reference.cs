﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceConsoleClient.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.ICloudBoxService")]
    public interface ICloudBoxService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICloudBoxService/GetData", ReplyAction="http://tempuri.org/ICloudBoxService/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICloudBoxService/GetData", ReplyAction="http://tempuri.org/ICloudBoxService/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICloudBoxService/ValidatePassword", ReplyAction="http://tempuri.org/ICloudBoxService/ValidatePasswordResponse")]
        bool ValidatePassword(string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICloudBoxService/ValidatePassword", ReplyAction="http://tempuri.org/ICloudBoxService/ValidatePasswordResponse")]
        System.Threading.Tasks.Task<bool> ValidatePasswordAsync(string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICloudBoxService/GetPasswordFromDb", ReplyAction="http://tempuri.org/ICloudBoxService/GetPasswordFromDbResponse")]
        string GetPasswordFromDb();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICloudBoxService/GetPasswordFromDb", ReplyAction="http://tempuri.org/ICloudBoxService/GetPasswordFromDbResponse")]
        System.Threading.Tasks.Task<string> GetPasswordFromDbAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICloudBoxServiceChannel : ServiceConsoleClient.ServiceReference1.ICloudBoxService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CloudBoxServiceClient : System.ServiceModel.ClientBase<ServiceConsoleClient.ServiceReference1.ICloudBoxService>, ServiceConsoleClient.ServiceReference1.ICloudBoxService {
        
        public CloudBoxServiceClient() {
        }
        
        public CloudBoxServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CloudBoxServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CloudBoxServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CloudBoxServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync(int value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public bool ValidatePassword(string password) {
            return base.Channel.ValidatePassword(password);
        }
        
        public System.Threading.Tasks.Task<bool> ValidatePasswordAsync(string password) {
            return base.Channel.ValidatePasswordAsync(password);
        }
        
        public string GetPasswordFromDb() {
            return base.Channel.GetPasswordFromDb();
        }
        
        public System.Threading.Tasks.Task<string> GetPasswordFromDbAsync() {
            return base.Channel.GetPasswordFromDbAsync();
        }
    }
}