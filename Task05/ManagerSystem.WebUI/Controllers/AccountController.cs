using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ManagerSystem.UserStore.BLL.DTO;
using ManagerSystem.UserStore.BLL.Interfaces;
using ManagerSystem.WebUI.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace ManagerSystem.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService => HttpContext.GetOwinContext().GetUserManager<IUserService>();
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                var userDto = new UserDTO {Name = model.Name, Password = model.Password};
                var claim = await UserService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Incorrect login or password");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                var userDto = new UserDTO
                { 
                    Name = model.Name,
                    Password = model.Password,
                    Role = "user"
                };

                var operationDetails = await UserService.CreateAsync(userDto);
                if (operationDetails.Succedeed)
                    return View("SuccessRegister");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Name = "Arseni Barsegyan",
                Password = "A123",
                Role = "admin"
            }, new List<string> {"user", "admin"});
        }
    }
}