using System.Collections.Generic;
using System.ServiceModel;

namespace CloudBoxService
{
    [ServiceContract]
    public interface ICloudBoxService
    {
        [OperationContract]
        bool ValidateUser(string username, string password);

        [OperationContract]
        bool UploadFilesToServer(string userName, IEnumerable<byte[]> filesCollection);
    }
}
