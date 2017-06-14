using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ManagerSystem.UserStore.BLL.DTO;
using ManagerSystem.UserStore.BLL.Infrastructure;
using ManagerSystem.UserStore.BLL.Interfaces;
using ManagerSystem.UserStore.DAL.Entities;
using ManagerSystem.UserStore.DAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace ManagerSystem.UserStore.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork UnitOfWork { get; }

        public UserService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<OperationDetails> CreateAsync(UserDTO userDto)
        {
            var user = await UnitOfWork.UserManager.FindByEmailAsync(userDto.Email);
            if (user != null) return new OperationDetails(false, "User with this login already exists", "Email");

            user = new ApplicationUser {Email = userDto.Email, UserName = userDto.Name};
            var result = await UnitOfWork.UserManager.CreateAsync(user, userDto.Password);
            if (result.Errors.Any())
                return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

            await UnitOfWork.UserManager.AddToRoleAsync(user.Id, userDto.Role);

            var clientProfile = new ClientProfile {Id = user.Id, Name = userDto.Name};
            UnitOfWork.ClientManager.Create(clientProfile);
            await UnitOfWork.SaveAsync();
            return new OperationDetails(true, "register successfull", "");
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            var user = await UnitOfWork.UserManager.FindAsync(userDto.Email, userDto.Password);

            if (user != null)
            {
                claim = await UnitOfWork.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return claim;
        }

        public async Task SetInitialData(UserDTO adminDto, IEnumerable<string> roles)
        {
            foreach (var roleName in roles)
            {
                var role = await UnitOfWork.RoleManager.FindByNameAsync(roleName);
                if (role != null) continue;
                role = new ApplicationRole {Name = roleName};
                await UnitOfWork.RoleManager.CreateAsync(role);
            }
            await CreateAsync(adminDto);
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}