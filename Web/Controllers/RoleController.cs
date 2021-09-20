using System;
using MediatR;
using Core.Dtos;
using AutoMapper;
using Shared.Enums;
using Web.ViewModel;
using Shared.Exceptions;
using Core.ModelResponse;
using Core.ModelResponse.One;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Core.Modules.TeamModule.Get;
using Core.Modules.RoleModule.Add;
using Core.Modules.RoleModule.List;
using Microsoft.AspNetCore.Identity;
using Core.Modules.TeamModule.Update;
using Core.Modules.RoleModule.Remove;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ActionResult> Index()
        {
            List<IdentityRole> role = await _mediator.Send(new ListRolesQuery());
            List<RoleDto> roles = new List<RoleDto>();
            foreach (IdentityRole item in role)
            {
                RoleDto rol = new RoleDto
                {
                    Id = item.Id,
                    Name = item.Name
                };
                roles.Add(rol);
            }

            return View(roles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddRoleCommand addRole)
        {
            try
            {
                await _mediator.Send(addRole);
                TempData["Title"] = "Registered";
                TempData["Message"] = $"The role: {addRole.Name} has been created";
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

        public async Task<ActionResult> Edit(int id)
        {

            ATeamResponse teamResponse = await _mediator.Send(new GetTeamByIdQuery { TeamId = id });
            TempData["Title"] = teamResponse.Data.Title;
            TempData["Message"] = teamResponse.Data.Message;
            TempData["State"] = teamResponse.Data.State.ToString();

            if (!teamResponse.Data.IsSuccess)
                return RedirectToAction(nameof(Index));

            TeamViewModels teamView = _mapper.Map<TeamViewModels>(teamResponse.Team);
            return View(teamView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TeamViewModels teamView)
        {
            TeamResponse teamResponse = _mapper.Map<TeamResponse>(teamView);
            ActionResponse update = await _mediator.Send(new UpdateTeamCommand { Team = teamResponse });
            TempData["Title"] = update.Title;
            TempData["Message"] = update.Message;
            TempData["State"] = update.State.ToString();

            if (!update.IsSuccess)
                return View(teamView);

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                bool delete = await _mediator.Send(new RemoveRoleCommand { Id = id });
                TempData["Title"] = "Deleted";
                TempData["Message"] = $"The role: {id} has been deleted";
                TempData["State"] = $"{State.success}";
            }
            catch (Exception)
            {
                TempData["Title"] = "Error";
                TempData["Message"] = $"The role: {id} has not been deleted";
                TempData["State"] = $"{State.error}";
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
