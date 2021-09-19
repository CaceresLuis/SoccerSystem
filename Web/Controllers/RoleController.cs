using System;
using MediatR;
using AutoMapper;
using Web.ViewModel;
using Core.ModelResponse;
using Core.ModelResponse.One;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.TeamModule.Get;
using Core.Modules.TeamModule.List;
using Core.Modules.TeamModule.Remove;
using Core.Modules.TeamModule.Update;
using Microsoft.AspNetCore.Authorization;
using Core.Modules.RoleModule.Add;
using Core.Modules.RoleModule.List;
using Core.Dtos;
using System.Collections.Generic;

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
            var  role = await _mediator.Send(new ListRolesQuery());
            List<RoleDto> roles = new List<RoleDto>();
            foreach (var item in role)
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
            }
            catch (Exception e)
            {
                TempData["Title"] = e.Message;
            }

            return RedirectToAction("Index", "Team");
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

        public async Task<ActionResult> Delete(int id)
        {
            if (id < 1) return NotFound();

            ActionResponse delete = await _mediator.Send(new RemoveTeamCommand { IdTeam = id });
            TempData["Title"] = delete.Title;
            TempData["Message"] = delete.Message;
            TempData["State"] = delete.State.ToString();

            return RedirectToAction(nameof(Index));
        }
    }
}
