using MediatR;
using Web.Models;
using AutoMapper;
using Web.ViewModel;
using Newtonsoft.Json;
using Core.ModelResponse;
using System.Threading.Tasks;
using Core.ModelResponse.One;
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
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public TournamentController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ActionResult> Index()
        {
            TournamentResponse[] tournaments = await _mediator.Send(new ListTournamentsQuery());
            Tournament[] tournament = _mapper.Map<Tournament[]>(tournaments);
            ListTournamentViewModel response = new ListTournamentViewModel { Tournaments = tournament };

            response.Data = new ActionResponse { };

            if (tournaments.Length < 1)
                return View(response);


            if (TempData["Data"] != null)
                response.Data = JsonConvert.DeserializeObject<ActionResponse>((string)TempData["Data"]);

            return View(response);
        }

        public ActionResult Create()
        {
            TournamentViewModel response = new TournamentViewModel { };
            response.Data = new ActionResponse { };
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Tournament tournament)
        {
            TournamentResponse tournamentResponse = _mapper.Map<TournamentResponse>(tournament);
            ActionResponse create = await _mediator.Send(new AddTournamentCommand { Tournament = tournamentResponse });

            if (!create.IsSuccess)
            {
                TournamentViewModel response = new TournamentViewModel { };
                response.Data = create;
                return View(response);
            }
                
            TempData["Data"] = JsonConvert.SerializeObject(create);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Details(int id)
        {
            if (id < 1) return NotFound();
            ATournamentResponse tournamentResponse = await _mediator.Send(new GetTournamentQuery { Id = id });
            Tournament tournament = _mapper.Map<Tournament>(tournamentResponse.Tournament);
            TournamentViewModel tournamentView = new TournamentViewModel { Tournament = tournament, Data = new ActionResponse { } };

            if (TempData["Data"] != null)
                tournamentView.Data = JsonConvert.DeserializeObject<ActionResponse>((string)TempData["Data"]);

            return View(tournamentView);
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (id < 1) return NotFound();

            ATournamentResponse tournamentResponse = await _mediator.Send(new GetTournamentQuery { Id = id });
            Tournament tournament = _mapper.Map<Tournament>(tournamentResponse.Tournament);
            TournamentViewModel tournamentView = new TournamentViewModel { Tournament = tournament, Data = new ActionResponse { } };

            if (TempData["Data"] != null)
                tournamentResponse.Data = JsonConvert.DeserializeObject<ActionResponse>((string)TempData["Data"]);

            return View(tournamentView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Tournament tournament)
        {
            tournament.Id = id;
            TournamentResponse tournamentResponse = _mapper.Map<TournamentResponse>(tournament);
            ActionResponse update = await _mediator.Send(new UpdateTournamentCommnad { TournamentResponse = tournamentResponse });
            if (!update.IsSuccess)
            {
                //Preparamos toda el modelo para reenviar a la vista
                TournamentViewModel tournamentView = new TournamentViewModel { Tournament = tournament, Data = update };
                return View(tournamentView);
            }

            TempData["Data"] = JsonConvert.SerializeObject(update);
            return  RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (id < 1) return NotFound();

            ActionResponse response = await _mediator.Send(new RemoveTournamentCommand { Id = id });
            if (!response.IsSuccess)
            {
                TempData["Data"] = JsonConvert.SerializeObject(response);
                return RedirectToAction(nameof(Index));
            }

            TempData["Data"] = JsonConvert.SerializeObject(response);
            return RedirectToAction(nameof(Index));
        }
    }
}
