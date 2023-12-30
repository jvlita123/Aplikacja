using Microsoft.AspNetCore.Http;
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

        public IActionResult Index()
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
            _servicesService.AddService(service);
            return View();
        }

        public IActionResult Edit(int id)
        {
            return NoContent();        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToActionResult Edit(int id, Data.Entities.Service collection)
        {
            _servicesService.Edit(id, collection);

            return RedirectToAction("Index");        
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            _servicesService.Remove(id);
            return NoContent();
        }
    }
}
