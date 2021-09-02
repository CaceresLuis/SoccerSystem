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

        public TournamentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> Index()
        {
            TournamentResponse[] tournaments = await _mediator.Send(new ListTournamentsQuery());
            if (tournaments.Length < 1)
                return View();

            ListTournamentResponse response = new ListTournamentResponse{ Tournaments = tournaments };
            response.Data = new ActionResponse { };

            if (TempData["Data"] != null)
                response.Data = JsonConvert.DeserializeObject<ActionResponse>((string)TempData["Data"]);

            return View(response);
        }

        public ActionResult Create()
        {
            TournamentResponse response = new TournamentResponse{ };
            response.Data = new ActionResponse { };
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TournamentResponse tournament)
        {
            ActionResponse create = await _mediator.Send(new AddTournamentCommand { Tournament = tournament });

            if (!create.IsSuccess)
            {
                TournamentResponse response = new TournamentResponse { };
                response.Data = create;

                return View(response);
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

            TournamentResponse response = await _mediator.Send(new GetTournamentQuery { Id = id });
            response.Data = new ActionResponse { };

            if (TempData["Data"] != null)
                response.Data = JsonConvert.DeserializeObject<ActionResponse>((string)TempData["Data"]);

            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TournamentResponse tournament)
        {
            tournament.Id = id;
            ActionResponse update = await _mediator.Send(new UpdateTournamentCommnad { TournamentResponse = tournament });
            if (!update.IsSuccess)
            {
                //Preparamos toda el modelo para reenviar a la vista
                TournamentResponse response = await _mediator.Send(new GetTournamentQuery { Id = id });
                response.Data = update;

                return View(response);
            }

            TempData["Data"] = JsonConvert.SerializeObject(update);
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
