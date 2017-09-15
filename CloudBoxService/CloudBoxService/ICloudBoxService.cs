using System.ServiceModel;

namespace CloudBoxService
{
    [ServiceContract]
    public interface ICloudBoxService
    {
        [OperationContract]
        bool ValidateUser(string username, string password);

        [OperationContract]
        bool UploadFilesToServer(string userName, string userPassword, byte[] fileContent);

        [OperationContract]
        bool CreateFolder(string userName, string userPassword, string folderName);
    }
}
