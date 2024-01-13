using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class ServicesController : Controller
    {
        private readonly Service.Services.ServiceService _servicesService;

        public ServicesController(Service.Services.ServiceService servicesService)
        {
            _servicesService = servicesService;
        }

        [Route("AllServices")]
        public ActionResult Index()
        {
            return View(_servicesService.GetAll());
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult AddService()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddService(Data.Entities.Service service)
        {
            if (!ModelState.IsValid)
            {
                return View(service);
            }
            _servicesService.AddService(service);
            return RedirectToAction("AllServices");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Data.Entities.Service collection)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                             .Select(e => e.ErrorMessage)
                                             .ToList();

                return BadRequest(new { Message = "Validation failed", Errors = errors });
            }
            _servicesService.Edit(collection.Id, collection);
            return Json(new { success = true, redirectUrl = Url.Action("Index", "Services") });
        }


        [HttpPost]
        public IActionResult Remove(int id)
        {
            _servicesService.Remove(id);
            return NoContent();
        }
    }
}
