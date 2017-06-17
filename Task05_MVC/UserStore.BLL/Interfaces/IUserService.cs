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
        Task<OperationDetails> CreateAsync(UserDto userDto);
        Task<ClaimsIdentity> Authenticate(UserDto userDto);
        Task SetInitialData(UserDto adminDto, IEnumerable<string> roles);
    }
}