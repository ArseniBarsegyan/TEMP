using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using UserStore.BLL.DTO;
using UserStore.BLL.Infrastructure;
using UserStore.BLL.Interfaces;
using UserStore.DAL.Entities;
using UserStore.DAL.Interfaces;

namespace UserStore.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork UnitOfWork { get; }

        public UserService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IEnumerable<UserDto> GetAllUsersList()
        {
            var userList = UnitOfWork.UserManager.Users.ToList();

            var userDtoList = userList.Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.UserName,
                Role = user.Roles.First().RoleId
            }).ToList();
            return userDtoList.AsEnumerable();
        }

        public async Task<OperationDetails> CreateAsync(UserDto userDto)
        {
            var user = await UnitOfWork.UserManager.FindByNameAsync(userDto.Name);
            if (user != null) return new OperationDetails(false, "User with this login already exists", "Name");

            user = new ApplicationUser { UserName = userDto.Name };
            var result = await UnitOfWork.UserManager.CreateAsync(user, userDto.Password);

            if (result.Errors.Any())
                return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

            await UnitOfWork.UserManager.AddToRoleAsync(user.Id, userDto.Role);
            await UnitOfWork.SaveAsync();
            return new OperationDetails(true, "register successfull", "");
        }

        public async Task<ClaimsIdentity> Authenticate(UserDto userDto)
        {
            ClaimsIdentity claim = null;
            var user = await UnitOfWork.UserManager.FindAsync(userDto.Name, userDto.Password);

            if (user != null)
            {
                claim = await UnitOfWork.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return claim;
        }

        public async Task SetInitialData(UserDto adminDto, IEnumerable<string> roles)
        {
            foreach (var roleName in roles)
            {
                var role = await UnitOfWork.RoleManager.FindByNameAsync(roleName);
                if (role != null) continue;
                role = new ApplicationRole { Name = roleName };
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