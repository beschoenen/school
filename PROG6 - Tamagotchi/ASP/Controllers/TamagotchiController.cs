using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ASP.TamagotchiService;

namespace ASP.Controllers
{
    public class TamagotchiController : Controller
    {
        private readonly ITamagotchiService _service;

        public TamagotchiController(ITamagotchiService service)
        {
            _service = service;
        }

        // GET: Tamagotchi
        public ActionResult Index()
        {
            var tamagotchis = _service.GetAllTamagotchi()
                .OrderBy(e => e.Deceased);

            ViewBag.CurrentTamagotchi = _service.GetCurrentTamagotchi();

            return View(tamagotchis);
        }

        // GET: Tamagotchi/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tamagotchi/Create
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(List<string> names)
        {
            names.ToList().ForEach(t =>
            {
                if (string.IsNullOrWhiteSpace(t)) return;

                var tamagotchi = new Tamagotchi {Name = t, Health = 100};

                _service.CreateTamagotchi(tamagotchi);
            });

            return RedirectToAction("Index");
        }

        // GET: Tamagotchi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tamagotchi = _service.GetTamagotchi(id.Value);

            if (tamagotchi == null) return HttpNotFound();

            return View(tamagotchi);
        }

        // POST: Tamagotchi/Delete/5
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _service.DeleteTamagotchi(id);

            return RedirectToAction("Index");
        }

        // GET: Tamagotchi/SetTamagotchi/5
        public ActionResult SetTamagotchi(int id)
        {
            var tamagotchi = _service.GetTamagotchi(id);

            if (tamagotchi == null) return HttpNotFound();

            _service.SwitchTamagotchi(tamagotchi.Id);

            return RedirectToAction("Play");
        }

        // Get: Tamagotchi/Dismiss
        public ActionResult Dismiss()
        {
            _service.DismissTamagotchi();

            return RedirectToAction("Index");
        }

        // GET: Tamagotchi/Play
        public ActionResult Play()
        {
            var tamagotchi = _service.GetCurrentTamagotchi();

            if (tamagotchi == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Tamagotchi = tamagotchi;
            ViewBag.Rules = _service.ActiveGameRules();

            return View(tamagotchi);
        }
    }
}
