using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PagedList;
using UserStore.BLL.DTO;
using UserStore.BLL.Interfaces;

namespace UserStore.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private IUserService UserService => HttpContext.GetOwinContext().GetUserManager<IUserService>();
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(UserService.GetAllUsersList().ToPagedList(pageNumber, pageSize));
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

        public ActionResult Delete(string id)
        {
            var userDto = UserService.GetById(id);
            if (userDto != null)
            {
                return View(userDto);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var operationDetails = await UserService.DeleteAsync(userDto);

                if (operationDetails.Succedeed)
                    return RedirectToAction("Index", "Admin");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View();
        }
    }
}