using MediatR;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.UserModule.Add;
using Core.Modules.UserModule.LoginWeb;
using Core.Modules.UserModule.Logout;
using Core.Modules.UserModule.Get;
using Core.Dtos;
using Core.Modules.UserModule.Update;
using Microsoft.AspNetCore.Authorization;
using Core.Modules.UserModule.List;
using System.Collections.Generic;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Index()
        {
            List<UserDto> users = await _mediator.Send(new ListUserQuery());
            return View(users);
        }


        [Authorize(Roles = "admin")]
        public ActionResult AddRoleToUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddRoleToUser(UserDto addUser)
        {
            await _mediator.Send(new AddUserCommand { UserDto = addUser });

            return RedirectToAction("Index", "Team");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserDto addUser)
        {
            await _mediator.Send(new AddUserCommand { UserDto = addUser });

            return RedirectToAction("Index", "Team");
        }

        public IActionResult Login()

        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginWebQuery model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _mediator.Send(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email or password incorrect.");
            }

            return View(model);
        }

        public async Task<ActionResult> Logout()
        {
            await _mediator.Send(new LogoutUserQuery { });
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "admin, user")]
        public async Task<ActionResult> MyProfile()
        {
            return View(await _mediator.Send(new GetMyProfileQuery { }));
        }

        [Authorize(Roles = "admin, user")]
        public async Task<ActionResult> Edit()
        {
            return View(await _mediator.Send(new GetMyProfileQuery { }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserDto addUser)
        {
            await _mediator.Send(new UpdateUserCommand { UserDto = addUser });

            return RedirectToAction("Index", "Team");
        }
    }
}
