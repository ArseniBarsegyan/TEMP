using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GameStore.Models;

namespace GameStore.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<Game> repository;
        UnitOfWork unitOfWork;

        public HomeController()
        {
            repository = new GameRepository<Game>(new GameContext());
            unitOfWork = new UnitOfWork();
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult About()
        {
            return View();
        }
        
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult GameList()
        {
            ViewBag.Games = unitOfWork.Games.GetGameList();
            return View();
        }

        [HttpPost]
        public ActionResult GameSearch(string name)
        {
            name = name.ToLower();
            IEnumerable<Game> gameList = unitOfWork.Games.GetGameList();
            Game game = gameList.FirstOrDefault(x => x.Name.ToLower() == name);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.Game = game;          
            return PartialView(game);
        }
    }
}