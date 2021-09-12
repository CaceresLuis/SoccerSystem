using MediatR;
using AutoMapper;
using Web.ViewModel;
using Core.ModelResponse;
using Core.ModelResponse.One;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.TeamModule.Add;
using Core.Modules.TeamModule.Get;
using Core.Modules.TeamModule.List;
using Core.Modules.TeamModule.Remove;
using Core.Modules.TeamModule.Update;

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
            TeamResponse[] teamResponse = await _mediator.Send(new ListTeamsQuery());

            TeamViewModel[] teamView = _mapper.Map<TeamViewModel[]>(teamResponse);

            return View(teamView);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TeamViewModel teamView)
        {
            TeamResponse teamResponse = _mapper.Map<TeamResponse>(teamView);
            ActionResponse create = await _mediator.Send(new AddTeamCommand { Team = teamResponse });
            TempData["Title"] = create.Title;
            TempData["Message"] = create.Message;
            TempData["State"] = create.State.ToString();

            if (!create.IsSuccess)
                return View();

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Edit(int id)
        {

            ATeamResponse teamResponse = await _mediator.Send(new GetTeamByIdQuery { TeamId = id });
            TempData["Title"] = teamResponse.Data.Title;
            TempData["Message"] = teamResponse.Data.Message;
            TempData["State"] = teamResponse.Data.State.ToString();

            if (!teamResponse.Data.IsSuccess)
                return RedirectToAction(nameof(Index));

            TeamViewModel teamView = _mapper.Map<TeamViewModel>(teamResponse.Team);
            return View(teamView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TeamViewModel teamView)
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
