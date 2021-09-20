using MediatR;
using Core.Dtos;
using System.Linq;
using Shared.Enums;
using Core.Helpers;
using Web.ModelsView;
using Shared.Exceptions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Core.Modules.UserModule.Add;
using Core.Modules.UserModule.Get;
using Core.Modules.UserModule.List;
using Core.Modules.UserModule.Logout;
using Core.Modules.UserModule.Update;
using Core.Modules.UserModule.LoginWeb;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IListItemHelper _listItemHelper;

        public AccountController(IMediator mediator, IListItemHelper listItemHelper)
        {
            _mediator = mediator;
            _listItemHelper = listItemHelper;
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Index()
        {
            List<UserDto> users = await _mediator.Send(new ListUserQuery());
            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserDto addUser)
        {
            try
            {
                await _mediator.Send(new AddUserCommand { UserDto = addUser });
                TempData["Title"] = "Registered";
                TempData["Message"] = $"The User: {addUser.Email} has been created";
                TempData["State"] = $"{State.success}";

                return RedirectToAction(nameof(Index));
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = e.Error.State;

                return View();
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> AddRoleToUser(string email)
        {
            var user = await _mediator.Send(new GetUserQuery { Email = email });
            AddRoleToUserViewModel addRoleToUser = new AddRoleToUserViewModel
            { 
                UserDto = user,
                SelectRol = await _listItemHelper.RolesListItem(user.Roles)
            };
            return View(addRoleToUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddRoleToUser(AddRoleToUserViewModel roleUserView)
        {
            try
            {
                await _mediator.Send(new AddRoleToUserCommand { Email = roleUserView.UserDto.Email, RoleName = roleUserView.RoleName });

                TempData["Title"] = "Succdess";
                TempData["Message"] = "User now have a new role";
                TempData["State"] = $"{State.success}";
                return RedirectToAction(nameof(Index));
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = e.Error.State;

                return View(roleUserView);
            }
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
            try
            {
                await _mediator.Send(new UpdateUserCommand { UserDto = addUser });

                TempData["Title"] = "Updated";
                TempData["Message"] = "your profile has been updated";
                TempData["State"] = $"{State.success}";

                return RedirectToAction(nameof(MyProfile));
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = e.Error.State;

                return View();
            }
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }
    }
}
