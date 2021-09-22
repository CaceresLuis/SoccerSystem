using MediatR;
using Core.Dtos;
using AutoMapper;
using Shared.Enums;
using Web.ViewModel;
using Shared.Exceptions;
using Core.ModelResponse;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.TeamModule.Add;
using Core.Modules.TeamModule.Get;
using Core.Modules.TeamModule.List;
using Core.Modules.TeamModule.Remove;
using Core.Modules.TeamModule.Update;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    public class TeamController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public TeamController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ActionResult> Index()
        {
            TeamDto[] teamDtos = await _mediator.Send(new ListTeamsQuery());

            return View(teamDtos);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TeamViewModels teamView)
        {
            try
            {
                TeamResponse teamResponse = _mapper.Map<TeamResponse>(teamView);
                await _mediator.Send(new AddTeamCommand { Team = teamResponse });
                TempData["Title"] = "Created";
                TempData["Message"] = $"The team {teamResponse.Name} was created";
                TempData["State"] = State.success.ToString();

                return RedirectToAction(nameof(Index));
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = State.error.ToString();
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                TeamDto teamDto = await _mediator.Send(new GetTeamByIdQuery { TeamId = id });
                return View(teamDto);
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = State.error.ToString();
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TeamViewModels teamView)
        {
            try
            {
                TeamResponse teamResponse = _mapper.Map<TeamResponse>(teamView);
                await _mediator.Send(new UpdateTeamCommand { Team = teamResponse });
                TempData["Title"] = "Updated!";
                TempData["Message"] = $"The team: {teamResponse.Name} was Updated";
                TempData["State"] = State.success.ToString();

                return RedirectToAction(nameof(Index));
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = State.error.ToString();
                return View(teamView);
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _mediator.Send(new RemoveTeamCommand { IdTeam = id });
                TempData["Title"] = "Deleted!";
                TempData["Message"] = $"Team has been deleted!";
                TempData["State"] = State.success.ToString();
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = State.error.ToString();
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
