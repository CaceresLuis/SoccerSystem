using MediatR;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.UserModule.Add;
using Core.Modules.UserModule.LoginWeb;
using Core.Modules.UserModule.Logout;
using Core.Modules.UserModule.Get;
using Core.Dtos;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddUserCommand addUser)
        {
            await _mediator.Send(addUser);

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
        
        public async Task<ActionResult> MyProfile()
        {
            UserDto user = await _mediator.Send(new GetMyProfileQuery { });

            return View(user);
        }
    }
}
