using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class MakeUpCreatorController : Controller
    {
        // GET: MakeUpCreatorController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MakeUpCreatorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MakeUpCreatorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MakeUpCreatorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MakeUpCreatorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MakeUpCreatorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MakeUpCreatorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MakeUpCreatorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
