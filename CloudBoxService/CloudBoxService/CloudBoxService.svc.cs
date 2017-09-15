using System.Collections.Generic;
using System.IO;
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

        public bool UploadFilesToServer(string userName, string password, byte[] fileContent)
        {
            MySqlSimpleMembershipProvider provider = new MySqlSimpleMembershipProvider();

            var loginResult = WebSecurity.Login(userName, password);
            var accountsDirectory = System.AppDomain.CurrentDomain.BaseDirectory + @"\Accounts";
            if (!Directory.Exists(accountsDirectory))
            {
                if (WebSecurity.Login(userName, password))
                {
                    var userDirectory = accountsDirectory + @"\" + userName;
                    if (!Directory.Exists(userDirectory))
                    {
                        Directory.CreateDirectory(userDirectory);
                    }
                }
            }
            return true;
        }
    }
}
