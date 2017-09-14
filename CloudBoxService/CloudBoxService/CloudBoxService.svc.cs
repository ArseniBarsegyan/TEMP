using System.Collections.Generic;
using MySql.Web.Security;
using WebMatrix.WebData;

namespace CloudBoxService
{
    public class CloudBoxService : ICloudBoxService
    {
        public bool ValidateUser(string username, string password)
        {
            MySqlSimpleMembershipProvider provider = new MySqlSimpleMembershipProvider();
           
            return WebSecurity.Login(username, password);
        }

        public bool UploadFilesToServer(string userName, IEnumerable<byte[]> filesCollection)
        {
            throw new System.NotImplementedException();
        }
    }
}
