using System.ServiceModel;
using System.Collections.Generic;
using System.Web;

namespace CloudBoxService
{
    [ServiceContract]
    public interface ICloudBoxService
    {
        [OperationContract]
        bool ValidateUser(string username, string password);

        [OperationContract]
        IEnumerable<string> GetAllDirectoriesByPath(string userName, string path);

        [OperationContract]
        IEnumerable<string> GetAllFilesByPath(string userName, string path);

        [OperationContract]
        void CheckIfDirectoryWithUserNameExists(string username);

        [OperationContract]
        bool UploadFilesToServer(string userName, string password, byte[] fileContent);

        [OperationContract]
        string CreateFolderIfNotExists(string path);

        [OperationContract]
        void RemoveElement(string path);

        [OperationContract]
        string Upload(byte[] file, string path);

        [OperationContract]
        string GetFileLink(string path);
    }
}
