using System;
using CloudBox.BLL.DTO;
using CloudBox.BLL.Infrastructure;

namespace CloudBox.BLL.Interfaces
{
    public interface IUserService
    {
        UserDto GetById(int id);
        OperationDetails Register(UserDto userDto);
        OperationDetails Login(UserDto userDto);
    }
}