using System.Configuration;
using MySql.Data.MySqlClient;
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
            return WebSecurity.Login("luke_lp@mail.ru", "luke465", false);
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
