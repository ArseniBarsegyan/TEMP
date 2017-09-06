using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace TestUpload.Controllers
{
    public class HomeController : Controller
    {
        //Index содержит текущую директорию пользователя
        //а также список все файлов в текущей директории
        public ActionResult Index()
        {
            var currentFolders = GetAllDirectoriesByUserName("SuperMan");
            ViewBag.CurrentPath = GetUserFolderRelativePath("SuperMan");
            ViewBag.Directories = currentFolders;
            ViewBag.Files = GetAllFilesByUserName("SuperMan");
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

        //Получаем абсолютный путь из переданных папок
        private string GetUserFolderRelativePath(string path)
        {
            return Server.MapPath("~/Files/" + path);
        }

        //Получаем папки из директории пользователя
        private IEnumerable<string> GetAllDirectoriesByUserName(string username)
        {
            CheckIfDirectoryWithUserNameExists(username);
            var directoriesPaths = Directory.GetDirectories(Server.MapPath("~/Files/" + username));
            for (var i = 0; i < directoriesPaths.Length; i++)
            {
                directoriesPaths[i] = new FileInfo(directoriesPaths[i]).Name;
            }
            return directoriesPaths;
        }

        //Получаем файлы из директории пользователя
        private IEnumerable<string> GetAllFilesByUserName(string username)
        {
            CheckIfDirectoryWithUserNameExists(username);
            var filesPaths = Directory.GetFiles(Server.MapPath("~/Files/" + username));
            for (var i = 0; i < filesPaths.Length; i++) 
            {
                filesPaths[i] = new FileInfo(filesPaths[i]).Name;
            }
            return filesPaths;
        }

        //Проверяем, существует ли папка пользователя. Если нет, создаем ее
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