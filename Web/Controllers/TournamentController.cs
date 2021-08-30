using Core.Modules.TeamModule.Get;
using Core.Modules.TeamModule.Remove;
using Core.Modules.TeamModule.Update;
using Core.Modules.TournamentModule.Add;
using Core.Modules.TournamentModule.List;
using Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.ViewModel;
using System;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class TournamentController : Controller
    {
        private readonly IMediator _mediator;

        public TournamentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: TeamController
        public async Task<ActionResult> Index()
        {
            TournamentEntity[] tournaments = await _mediator.Send(new ListTournamentsQuery());
            if (tournaments.Length < 1)
                return View();

            return View(tournaments);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddTournamentCommand team)
        {
            if (await _mediator.Send(team))
                return RedirectToAction(nameof(Index));

            return View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (id < 1) return NotFound();

            return View(await _mediator.Send(new GetTeamByIdQuery { TeamId = id }));
        }

        // POST: TeamController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TeamViewModel team)
        {
            team.Id = id;
            if (!await _mediator.Send(new UpdateTeamCommand { TeamViewModel = team }))
                throw new Exception("algo ha salido mal");

            return  RedirectToAction(nameof(Index));
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
