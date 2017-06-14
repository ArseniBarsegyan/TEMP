using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ManagerSystem.UserStore.BLL.DTO;
using ManagerSystem.UserStore.BLL.Infrastructure;

namespace ManagerSystem.UserStore.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> CreateAsync(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, IEnumerable<string> roles);
    }
}