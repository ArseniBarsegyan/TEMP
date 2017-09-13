using System.Configuration;
using MySql.Data.MySqlClient;
using MySql.Web.Security;
using WebMatrix.WebData;

namespace CloudBoxService
{
    public class CloudBoxService : ICloudBoxService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public bool ValidatePassword(string password)
        {
            //WebSecurity.InitializeDatabaseConnection("MyConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
            MySqlSimpleMembershipProvider provider1 = new MySqlSimpleMembershipProvider();
           
            return WebSecurity.Login("luke_lp@mail.ru", "luke465");
        }

        public string GetPasswordFromDb()
        {
            using (MySqlConnection connection =
                    new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString))
            {
                connection.Open();
                string password = string.Empty;
                MySqlCommand command = new MySqlCommand("SELECT Password FROM webpages_membership", connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        password = reader[0].ToString();
                    }
                }
                return password;
            }
        }
    }
}
