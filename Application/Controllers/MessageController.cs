using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace Application.Controllers
{
    public class MessageController : Controller
    {

        private readonly ReservationService _reservationService;
        private readonly UserService _userService;
        private readonly ServiceService _serviceService;
        private readonly StatusService _statusService;
        private readonly EnrollmentsService _enrollmentService;
        private readonly CoursesService _coursesService;
        private readonly CyclesService _cyclesService;
        private readonly MessageService _messageService;

        public MessageController(MessageService messageService, CyclesService cyclesService, ReservationService reservationService, CoursesService coursesService, ServiceService serviceService, StatusService statusService, UserService userService, EnrollmentsService enrollmentsService)
        {
            _reservationService = reservationService;
            _userService = userService;
            _serviceService = serviceService;
            _statusService = statusService;
            _enrollmentService = enrollmentsService;
            _coursesService = coursesService;
            _cyclesService = cyclesService;
            _messageService = messageService;
        }

        public IActionResult Index()
        {
            ViewData["users"] = _userService.GetAll();

            List<Message> messages = _messageService.GetAll();

            return View(messages);
        }


        [HttpPost]
        [Authorize]
        public RedirectToActionResult SendMessage(int UserId2, string text)
        {
            User? user = _userService.GetByEmail(HttpContext.User.Identity.Name);
            _messageService.SendMessage(user.Id, UserId2, text);

            return RedirectToAction("GetConversation", new { id = UserId2 });
        }

        [HttpGet]
        public IActionResult GetReceivedMessages()
        {
            User user = _userService.GetByEmail(HttpContext.User.Identity.Name);
            List<Message> messages2 = _messageService.GetUserMessages(user.Id);

            return View(messages2);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetSentMessages()
        {
            User user = _userService.GetByEmail(HttpContext.User.Identity.Name);
            List<Message> messages = _messageService.GetSentMessages(user.Id);

            return View(messages);
        }

        [HttpGet]
        public IActionResult GetUserConversations()
        {
            User user = _userService.GetByEmail(HttpContext.User.Identity.Name);
            List<User> userConversation = _messageService.GetUserConversations(user.Id);
            return View(userConversation);
        }

        [HttpGet]
        public PartialViewResult GetConversation(int id)
        {
            User user = _userService.GetByEmail(HttpContext.User.Identity.Name);

            List<Message> conversation = new List<Message>();
            conversation = _messageService.GetConversation(user.Id, id);

            return PartialView(conversation);
        }

        [HttpGet]
        public IActionResult Messenger()
        {
            User user = _userService.GetByEmail(HttpContext.User.Identity.Name);
            List<User> userConversation = _messageService.GetUserConversations(user.Id);

            ViewData["users"] = userConversation;
            List<Message> conversation = _messageService.GetAll();

            return View(conversation);
        }
    }
}
