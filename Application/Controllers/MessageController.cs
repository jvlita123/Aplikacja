using Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using System.Security.Claims;

namespace Application.Controllers
{
    public class MessageController : Controller
    {
        private readonly UserService _userService;
        private readonly MessageService _messageService;

        public MessageController(MessageService messageService, UserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }

        [HttpPost]
        [Authorize]
        public RedirectToActionResult SendMessage(int UserId2, string Text)
        {
            User? user = _userService.GetByEmail(HttpContext.User.Identity.Name);
            _messageService.SendMessage(user.Id, UserId2, Text);

            var user1 = HttpContext.User;

            if (user1 != null)
            {
                var claimsIdentity = (ClaimsIdentity)user1.Identity;

                var existingNewMessageClaim = claimsIdentity.FindFirst("newMessage");
                if (existingNewMessageClaim != null)
                {
                    claimsIdentity.RemoveClaim(existingNewMessageClaim);
                }
                claimsIdentity.AddClaim(new Claim("newMessage", "true"));
                HttpContext.SignInAsync(HttpContext.User);
            }

            return RedirectToAction("GetConversation", new { id = UserId2 });
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUserConversations()
        {
            User user = _userService.GetByEmail(HttpContext.User.Identity.Name);
            List<User> userConversation = _messageService.GetUserConversations(user.Id);
            return PartialView("GetUserConversations", userConversation);
        }

        [HttpPost]
        [Authorize]
        public ViewResult GetConversation()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        [Route("GetConversation")]
        public IActionResult GetConversation(int id)
        {
            User user = _userService.GetByEmail(HttpContext.User.Identity.Name);
            List<User> userConversation = _messageService.GetUserConversations(user.Id);
            List<User> allAppUsers = _userService.GetAll();
            List<Message>? conversation = _messageService.GetConversation(user.Id, id);
            User user2 = _userService.GetById(id);

            var user1 = HttpContext.User;

            if (user1 != null && !_messageService.GetAll().Any(x => x.UserId2 == user.Id && x.IsNew == true))
            {
                var claimsIdentity = (ClaimsIdentity)user1.Identity;

                var existingNewMessageClaim = claimsIdentity.FindFirst("newMessage");
                if (existingNewMessageClaim != null)
                {
                    claimsIdentity.RemoveClaim(existingNewMessageClaim);
                }

                claimsIdentity.AddClaim(new Claim("newMessage", "false"));

                HttpContext.SignInAsync(HttpContext.User);
            }

            ViewData["users"] = userConversation;
            ViewData["allusers"] = allAppUsers;
            ViewData["user2"] = user2;
            ViewData["user1"] = user;

            return View(conversation);
        }
    }
}
