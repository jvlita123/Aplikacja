using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Service.Services;
using System.Security.Claims;

namespace Application.Controllers
{
    public class MessageController : Controller
    {
        private readonly UserService _userService;
        private readonly ServiceService _serviceService;
        private readonly StatusService _statusService;
        private readonly EnrollmentsService _enrollmentService;
        private readonly CoursesService _coursesService;
        private readonly CyclesService _cyclesService;
        private readonly MessageService _messageService;

        public MessageController(MessageService messageService, CyclesService cyclesService, CoursesService coursesService, ServiceService serviceService, StatusService statusService, UserService userService, EnrollmentsService enrollmentsService)
        {
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
            User user = _userService.GetByEmail(HttpContext.User.Identity.Name);


            List<Message> messages = _messageService.GetAll();

            return View();
        }


        [HttpPost]
        [Authorize]
        public async Task<RedirectToActionResult> SendMessageAsync(int UserId2, string Text)
        {
            User? user = _userService.GetByEmail(HttpContext.User.Identity.Name);
            _messageService.SendMessage(user.Id, UserId2, Text);
            
            var user1 = HttpContext.User;

            if (user1 != null)
            {
                var claimsIdentity = (ClaimsIdentity)user1.Identity;

                // Usunięcie starego claimu 'newMessage', jeśli istnieje
                var existingNewMessageClaim = claimsIdentity.FindFirst("newMessage");
                if (existingNewMessageClaim != null)
                {
                    claimsIdentity.RemoveClaim(existingNewMessageClaim);
                }

                // Dodanie nowego claimu 'newMessage' z nową wartością
                claimsIdentity.AddClaim(new Claim("newMessage", "true"));

                // Zaktualizowanie informacji o uwierzytelnieniu
                await HttpContext.SignInAsync(HttpContext.User);
            }

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
        public async Task<IActionResult> GetUserConversations()
        {
            User user = _userService.GetByEmail(HttpContext.User.Identity.Name);
            List<User> userConversation = _messageService.GetUserConversations(user.Id);
            return PartialView("GetUserConversations",userConversation);
        }

        [HttpGet]
        public async Task<ViewResult> GetConversationAsync(int id)
        {
            User user = _userService.GetByEmail(HttpContext.User.Identity.Name);
             List<User> userConversation = _messageService.GetUserConversations(user.Id);
            List<User> allAppUsers = _userService.GetAll();
            List<Message> conversation = _messageService.GetConversation(user.Id, id);
            User user2 = _userService.GetById(id);

            var user1 = HttpContext.User;

            if (user1 != null && !_messageService.GetAll().Any(x => x.UserId2 == user.Id && x.IsNew == true))
            {
                var claimsIdentity = (ClaimsIdentity)user1.Identity;

                // Usunięcie starego claimu 'newMessage', jeśli istnieje
                var existingNewMessageClaim = claimsIdentity.FindFirst("newMessage");
                if (existingNewMessageClaim != null)
                {
                    claimsIdentity.RemoveClaim(existingNewMessageClaim);
                }

                // Dodanie nowego claimu 'newMessage' z nową wartością
                claimsIdentity.AddClaim(new Claim("newMessage", "false"));

                // Zaktualizowanie informacji o uwierzytelnieniu
                await HttpContext.SignInAsync(HttpContext.User);
            }


            ViewData["users"] = userConversation;
            ViewData["allusers"] = allAppUsers;
            ViewData["user2"] = user2;
            ViewData["user1"] = user;
     
            return View(conversation);
        }

        [HttpPost]
        public ViewResult GetConversation()
        {
            return View();
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
