using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace TestUpload.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Directories = GetAllDirectoriesByUserName("SuperMan");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private IEnumerable<string> GetAllDirectoriesByUserName(string username)
        {
            CheckIfDirectoryWithUserNameExists(username);
            return Directory.GetDirectories(Server.MapPath("~/Files/" + username));
        }

        private void CheckIfDirectoryWithUserNameExists(string username)
        {
            var usersDirectories = Directory.GetDirectories(Server.MapPath("~/Files/"));
            var directoriesNames = usersDirectories.Select(directoryFullName => new FileInfo(directoryFullName).Name);
            if (!directoriesNames.Contains(username))
            {
                Directory.CreateDirectory(Server.MapPath("~/Files/" + username));
            }
        }
    }
}