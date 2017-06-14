using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ManagerSystem.BLL.Infrastructure;
using ManagerSystem.BLL.DTO;

namespace ManagerSystem.BLL.Interfaces
{
    public interface IUserService
    {
        Task<OperationDetails> Create(UserDto userDto);
        Task<ClaimsIdentity> Authenticate(UserDto userDto);
        Task SetInitialData(UserDto adminDto, List<string> roles);
    }
}