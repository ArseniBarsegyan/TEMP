using System.Collections.Generic;
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

        [OperationContract]
        bool UploadFilesToServer(IEnumerable<byte[]> filesCollection);
    }
}
