using System.Web.Mvc;
using GameStore.Models;

namespace GameStore.Controllers
{
    public class GameController : Controller
    {
        private IRepository<Game> repository;
        UnitOfWork unitOfWork;

        public GameController()
        {
            repository = new GameRepository<Game>(new GameContext());
            unitOfWork = new UnitOfWork();            
        }

        [Authorize(Roles = "admin")]
        public ActionResult Catalogue()
        {
            ViewBag.Games = unitOfWork.Games.GetGameList();
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(Game game)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Games.Create(game);
                unitOfWork.Save();
                return RedirectToAction("Catalogue");
            }
            return View(game);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Correct(int id)
        {
            Game game = unitOfWork.Games.GetGame(id);            
            return View(game);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Correct(Game game)
        {            
            unitOfWork.Games.Correct(game);
            unitOfWork.Save();
            return RedirectToAction("Catalogue");            
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Game game = unitOfWork.Games.GetGame(id);
            return View(game);
        }

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            unitOfWork.Games.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Catalogue");
        }

    }
}