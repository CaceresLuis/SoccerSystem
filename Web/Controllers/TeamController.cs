using MediatR;
using AutoMapper;
using Web.Models;
using Web.ViewModel;
using Newtonsoft.Json;
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

            Team[] team = _mapper.Map<Team[]>(teamResponse);

            ListTeamViewModel response = new ListTeamViewModel { Teams = team };
            response.Data = new ActionResponse { };

            if(teamResponse.Length <= 1)
                return View(response);


            if (TempData["Data"] != null)
                response.Data = JsonConvert.DeserializeObject<ActionResponse>((string)TempData["Data"]);

            return View(response);
        }

        public ActionResult Create()
        {
            TeamViewModel response = new TeamViewModel { };
            response.Data = new ActionResponse { };
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Team team)
        {
            TeamResponse teamResponse = _mapper.Map<TeamResponse>(team);
            ActionResponse create = await _mediator.Send(new AddTeamCommand { Team = teamResponse });
            if (!create.IsSuccess)
            {
                TeamViewModel response = new TeamViewModel { };
                response.Data = create;
                return View(response);
            }

            TempData["Data"] = JsonConvert.SerializeObject(create);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (id < 1) return NotFound();

            ATeamResponse teamResponse = await _mediator.Send(new GetTeamByIdQuery { TeamId = id });

            TeamViewModel teamView = new TeamViewModel { Team = _mapper.Map<Team>(teamResponse.Team), Data = teamResponse.Data };

            return View(teamView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TeamResponse team)
        {
            team.Id = id;
            ActionResponse update = await _mediator.Send(new UpdateTeamCommand { Team = team });
            if (!update.IsSuccess)
            {
                ATeamResponse response = await _mediator.Send(new GetTeamByIdQuery { TeamId = id });
                TeamViewModel teamView = new TeamViewModel { Team = _mapper.Map<Team>(response.Team), Data = update };
                return View(teamView);
            }

            TempData["Data"] = JsonConvert.SerializeObject(update);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (id < 1) return NotFound();

            ActionResponse response = await _mediator.Send(new RemoveTeamCommand { IdTeam = id });
            if (response.IsSuccess)
            {
                TempData["Data"] = JsonConvert.SerializeObject(response);
                return RedirectToAction(nameof(Index));
            }

            TempData["Data"] = JsonConvert.SerializeObject(response);
            return RedirectToAction(nameof(Index));
        }
    }
}
