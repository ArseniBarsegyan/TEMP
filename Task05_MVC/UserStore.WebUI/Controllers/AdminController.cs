using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using UserStore.BLL.DTO;
using UserStore.BLL.Interfaces;
using UserStore.WebUI.Models;

namespace UserStore.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private IUserService UserService => HttpContext.GetOwinContext().GetUserManager<IUserService>();
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public ActionResult Index()
        {
            return View(UserService.GetAllUsersList());
        }
    
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var userDto = new UserDto
                {
                    Name = model.Name,
                    Password = model.Password,
                    Role = "user"
                };

                var operationDetails = await UserService.CreateAsync(userDto);

                if (operationDetails.Succedeed)
                    return RedirectToAction("Index", "Admin");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }
        
        public ActionResult Edit(string id)
        {
            var userDto = UserService.GetById(id);
            if (userDto != null)
            {
                return View(userDto);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var operationDetails = await UserService.EditAsync(userDto);

                if (operationDetails.Succedeed)
                    return RedirectToAction("Index", "Admin");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View();
        }
    }
}