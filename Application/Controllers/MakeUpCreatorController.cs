using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class MakeUpCreatorController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
