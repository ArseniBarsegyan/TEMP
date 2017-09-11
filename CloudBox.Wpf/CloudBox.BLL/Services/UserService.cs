using System.Data;
using System.Web.Security;
using CloudBox.BLL.DTO;
using CloudBox.BLL.Infrastructure;
using CloudBox.BLL.Interfaces;
using MySql.Data.MySqlClient;

namespace CloudBox.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly string _connectionString;

        public UserService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public UserDto GetById(int id)
        {
            var userDto = new UserDto();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("GetUserById", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@userID", id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    userDto.Name = reader[0].ToString();
                    userDto.Password = reader[1].ToString();
                }
                return userDto;
            }
        }

        //Create user if doesn't exists
        public OperationDetails Register(UserDto userDto)
        {
            Membership.CreateUser(userDto.Name, userDto.Password);
            return new OperationDetails(true, "Register successful", string.Empty);
        }

        //If password correct log in
        public OperationDetails Login(UserDto userDto)
        {
            if (Membership.ValidateUser(userDto.Name, userDto.Password))
            {
                return new OperationDetails(true, "Login successful", string.Empty);
            }
            return new OperationDetails(false, "Incorrect name or password", string.Empty);
        }
    }
}