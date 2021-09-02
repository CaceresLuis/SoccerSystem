using System;
using MediatR;
using Newtonsoft.Json;
using Core.ModelResponse;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.TournamentModule.Add;
using Core.Modules.TournamentModule.Get;
using Core.Modules.TournamentModule.List;
using Core.Modules.TournamentModule.Remove;
using Core.Modules.TournamentModule.Update;

namespace Web.Controllers
{
    public class TournamentController : Controller
    {
        private readonly IMediator _mediator;
        private ActionResponse Data { get; set; }

        public TournamentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> Index()
        {
            TournamentResponse[] tournaments = await _mediator.Send(new ListTournamentsQuery());
            if (tournaments.Length < 1)
                return View();

            var response = new ListTournamentResponse{ Tournaments = tournaments };

            var actionResponse = new ActionResponse { Message = "Free" };
            
            response.Data = actionResponse;

            if (TempData["Data"] != null)
                response.Data = JsonConvert.DeserializeObject<ActionResponse>((string)TempData["Data"]);


            return View(response);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TournamentResponse tournament)
        {
            var create = await _mediator.Send(new AddTournamentCommand { Tournament = tournament });

            if (!create.IsSuccess)
            {
                TempData["Data"] = JsonConvert.SerializeObject(create);
                return View();
            }
                
            TempData["Data"] = JsonConvert.SerializeObject(create);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Details(int id)
        {
            if (id < 1) return NotFound();

            return View(await _mediator.Send(new GetTournamentQuery { Id = id }));
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (id < 1) return NotFound();

            return View(await _mediator.Send(new GetTournamentQuery { Id = id }));
        }

        // POST: TeamController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TournamentResponse tournament)
        {
            tournament.Id = id;
            if (!await _mediator.Send(new UpdateTournamentCommnad { TournamentResponse = tournament }))
                throw new Exception("algo ha salido mal");

            return  RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (id < 1) return NotFound();

            ActionResponse remove = await _mediator.Send(new RemoveTournamentCommand { Id = id });
            if (!remove.IsSuccess)
            {
                TempData["Data"] = JsonConvert.SerializeObject(remove);
                return RedirectToAction(nameof(Index));
            }

            TempData["Data"] = JsonConvert.SerializeObject(remove);

            return RedirectToAction(nameof(Index));
        }
    }
}
