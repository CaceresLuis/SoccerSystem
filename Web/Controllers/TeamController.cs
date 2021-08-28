using Core.Modules.TeamModule.Add;
using Core.Modules.TeamModule.Get;
using Core.Modules.TeamModule.List;
using Core.Modules.TeamModule.Update;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return View(await _mediator.Send(new ListTeamsQuery()));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddTeamCommand team)
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
            if (await _mediator.Send(new UpdateTeamCommand { Name = team.Name }))
                throw new Exception("algo ha salido mal");
            return  RedirectToAction(nameof(Index));
        }

        // GET: TeamController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TeamController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
