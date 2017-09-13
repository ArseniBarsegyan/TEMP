using System.ServiceModel;

namespace CloudBoxService
{
    [ServiceContract]
    public interface ICloudBoxService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        bool ValidatePassword(string password);

        [OperationContract]
        string GetPasswordFromDb();
    }
}
