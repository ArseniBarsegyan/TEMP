using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using UserStore.BLL.DTO;
using UserStore.BLL.Infrastructure;

namespace UserStore.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        UserDto GetById(string id);
        IEnumerable<UserDto> GetAllUsersList();
        Task<OperationDetails> CreateAsync(UserDto userDto);
        Task<OperationDetails> EditAsync(UserDto userDto);
        Task<OperationDetails> DeleteAsync(UserDto userDto);
        Task<ClaimsIdentity> Authenticate(UserDto userDto);
        Task SetInitialData(UserDto adminDto, IEnumerable<string> roles);
    }
}