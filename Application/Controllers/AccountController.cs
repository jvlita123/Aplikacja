using Application.Entities;
using Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
	public class AccountController : Controller
	{
		private AccountService _accountService;

		public AccountController(AccountService accountService)
		{
			_accountService = accountService;
		}

		[HttpGet]
		public IActionResult Index()
		{
			List<Account> accounts = _accountService.GetAll();

			return View(accounts);
		}

		[HttpGet]
		public IActionResult Get(int id)
		{
			Account account = _accountService.Get(id);

			return View();
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Account account)
		{
			_accountService.Add(account);

			return View();
		}

	}
}
