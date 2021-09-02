using System;
using MediatR;
using Newtonsoft.Json;
using Shared.ViewModel;
using Core.ModelResponse;
using Core.ModelResponse.One;
using System.Threading.Tasks;
using Core.ModelResponse.Lists;
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
        private readonly IMediator _mediator;

        public TeamController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: TeamController
        public async Task<ActionResult> Index()
        {
            Team[] team = await _mediator.Send(new ListTeamsQuery());

            ListTeamResponse response = new ListTeamResponse { Teams = team };
            response.Data = new ActionResponse { };

            if(team.Length <= 1)
                return View(response);


            if (TempData["Data"] != null)
                response.Data = JsonConvert.DeserializeObject<ActionResponse>((string)TempData["Data"]);

            return View(response);
        }

        public ActionResult Create()
        {
            OneTeamResponse response = new OneTeamResponse { };
            response.Data = new ActionResponse { };
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Team team)
        {
            ActionResponse create = await _mediator.Send(new AddTeamCommand { Team = team });
            if (!create.IsSuccess)
            {
                OneTeamResponse response = new OneTeamResponse { };
                response.Data = create;
                return View(response);
            }

            TempData["Data"] = JsonConvert.SerializeObject(create);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (id < 1) return NotFound();

            OneTeamResponse team = await _mediator.Send(new GetTeamByIdQuery { TeamId = id });

            return View(team);
        }

        // POST: TeamController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Team team)
        {
            team.Id = id;
            ActionResponse update = await _mediator.Send(new UpdateTeamCommand { Team = team });
            if (!update.IsSuccess)
            {
                OneTeamResponse response = await _mediator.Send(new GetTeamByIdQuery { TeamId = id });
                response.Data = update;
                return View(response);
            }

            TempData["Data"] = JsonConvert.SerializeObject(update);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (id < 1) return NotFound();

            if (!await _mediator.Send(new RemoveTeamCommand { IdTeam = id }))
                throw new Exception("Algo salio mal");

            return RedirectToAction(nameof(Index));
        }
    }
}
